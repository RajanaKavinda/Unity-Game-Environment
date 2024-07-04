using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

// Singleton class responsible for monitoring energy consumption and notifying observers
public class EnergyStatusController : MonoBehaviour
{
    private static EnergyStatusController instance;
    private List<IObserver> observers = new List<IObserver>();

    // Singleton instance
    public static EnergyStatusController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnergyStatusController>();
                if (instance == null)
                {
                    GameObject go = new GameObject("EnergyStatusController");
                    instance = go.AddComponent<EnergyStatusController>();
                }
            }
            return instance;
        }
    }

    private float lastEnergyReading;
    private float currentEnergyReading;
    private DateTime lastTime;
    private string jwtToken = GetMethod.jwtToken;
    private string urlExtension1 = "/power-consumption/current/view";
    public float averageEnergyConsumption;
    public int fruitsOrFlowers = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(UpdatePowerConsumption());
    }

    // Register an observer
    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    // Remove an observer
    public void RemoveObserver(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    // Notify all observers about the data update
    private void NotifyObservers()
    {
        // Remove any null references from the observers list
        observers.RemoveAll(observer => observer == null);

        foreach (var observer in observers)
        {
            observer.UpdateData(averageEnergyConsumption, fruitsOrFlowers);
        }
    }

    // Coroutine to periodically update power consumption
    private IEnumerator UpdatePowerConsumption()
    {
        HttpRequest httpRequest = new HttpRequest();

        while (true)
        {
            if (lastTime != DateTime.MinValue)
            {
                DateTime currentTime = DateTime.Now;
                // Send HTTP request to obtain current energy consumption
                yield return StartCoroutine(httpRequest.SendHttpRequest("get", "givenBackend", urlExtension1, jwtToken, ""));
                string result = (string)httpRequest.result;
                if (result == "Error")
                {
                    Debug.LogError("Error checking profile and questionnaire");
                    yield break;
                }

                // Calculate Average energy for 10s
                CurrentConsumptionResponse jsonData = JsonUtility.FromJson<CurrentConsumptionResponse>(result);
                currentEnergyReading = jsonData.currentConsumption;

                TimeSpan timeDifference = currentTime - lastTime;
                float timeDifferenceInSeconds = (float)timeDifference.TotalSeconds;

                float energyConsumptionFromLastTime = currentEnergyReading - lastEnergyReading;
                averageEnergyConsumption = energyConsumptionFromLastTime * 10 / timeDifferenceInSeconds;
                Debug.Log("Average energy consumption: " + averageEnergyConsumption);

                // Update valuse of FruitsOrFlower
                UpdateFruitsOrFlowers(averageEnergyConsumption);
                NotifyObservers();

                // Reset lastTime and lastEnergyReading
                lastTime = currentTime;
                lastEnergyReading = currentEnergyReading;

                yield return new WaitForSeconds(10f);
            }
            else
            {
                // Initiate lastTime and lastEnergyReading
                yield return StartCoroutine(httpRequest.SendHttpRequest("get", "givenBackend", urlExtension1, jwtToken, ""));
                string result = (string)httpRequest.result;
                if (result == "Error")
                {
                    Debug.LogError("Error checking profile and questionnaire");
                    yield break;
                }

                CurrentConsumptionResponse jsonData = JsonUtility.FromJson<CurrentConsumptionResponse>(result);
                lastEnergyReading = jsonData.currentConsumption;

                lastTime = DateTime.Now;
            }
        }
    }

    public class CurrentConsumptionResponse
    {
        public float currentConsumption;
    }

    // Update the number of fruits or flowers based on energy consumption
    private void UpdateFruitsOrFlowers(float averageEnergyConsumption)
    {
        if (averageEnergyConsumption < 0.1)
        {
            fruitsOrFlowers += 5;
        }
        else if (averageEnergyConsumption < 0.4)
        {
            fruitsOrFlowers += 3;
        }
        else if (averageEnergyConsumption < 0.8)
        {
            fruitsOrFlowers += 1;
        }
        else if (averageEnergyConsumption < 0.9)
        {
            fruitsOrFlowers -= 1;
        }
        else if (averageEnergyConsumption < 1)
        {
            fruitsOrFlowers -= 3;
        }
        else
        {
            fruitsOrFlowers -= 5;
        }
        Debug.Log("Total fruits or flowers: " + fruitsOrFlowers);
    }
}
