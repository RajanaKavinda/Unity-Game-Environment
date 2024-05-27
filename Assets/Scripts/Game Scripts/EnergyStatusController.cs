using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class EnergyStatusController : MonoBehaviour
{
    private List<TreeController> treeControllers; // List of TreeController references
    private List<LightingController> lightingControllers; // List of LightingController references

    private float lastEnergyReading;
    private float currentEnergyReading;
    private DateTime lastTime; 
    private string jwtToken = GetMethod.jwtToken;
    private string urlExtension1 = "/power-consumption/current/view";
    public float averageEnergyConsumption;
    public int fruitsOrFlowers = 0;

    private void Start()
    {
        // Find all TreeController instances in the scene
        treeControllers = new List<TreeController>(FindObjectsOfType<TreeController>());
        if (treeControllers.Count == 0)
        {
            Debug.LogError("No TreeController instances found in the scene.");
        }
        else
        {
            Debug.Log(treeControllers.Count + " TreeController instances found in the scene.");
        }

        // Find all LightingController instances in the scene
        lightingControllers = new List<LightingController>(FindObjectsOfType<LightingController>());
        if (lightingControllers.Count == 0)
        {
            Debug.LogError("No LightingController instances found in the scene.");
        }
        else
        {
            Debug.Log(lightingControllers.Count + " LightingController instances found in the scene.");
        }

        StartCoroutine(UpdatePowerConsumption());
    }

    IEnumerator UpdatePowerConsumption()
    {
        HttpRequest httpRequest = new HttpRequest();

        while (true)
        {
            
            if (lastTime != DateTime.MinValue)
            {
                DateTime currentTime = DateTime.Now;
                
                yield return StartCoroutine(httpRequest.SendHttpRequest("get", "givenBackend", urlExtension1, jwtToken, ""));
                string result = (string)httpRequest.result;
                if (result == "Error")
                {
                    Debug.LogError("Error checking profile and questionnaire");
                    yield break;
                }

                CurrentConsumptionResponse jsonData = JsonUtility.FromJson<CurrentConsumptionResponse>(result);
                currentEnergyReading = jsonData.currentConsumption; 
                
                TimeSpan timeDifference = currentTime - lastTime;
                float timeDifferenceInSeconds = (float)timeDifference.TotalSeconds;

                float energyConsumptionFromLastTime = currentEnergyReading - lastEnergyReading;
                averageEnergyConsumption = energyConsumptionFromLastTime * 10 / timeDifferenceInSeconds;
                Debug.Log("Average energy consumption: " + averageEnergyConsumption);
                UpdateFruitsOrFlowers(averageEnergyConsumption);
                UpdateLighting(averageEnergyConsumption);

                lastTime = currentTime;
                lastEnergyReading = currentEnergyReading;

                yield return new WaitForSeconds(10f);
            }
            else
            {
                
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

        // Notify each TreeController about the updated fruitsOrFlowers
        foreach (var treeController in treeControllers)
        {
            if (treeController != null)
            {
                
                treeController.UpdateTree(fruitsOrFlowers);
            }
            else
            {
                Debug.LogError("TreeController reference is null.");
            }
        }
    }

    private void UpdateLighting(float averageEnergyConsumption)
    {
        
        // Notify each LightingController about the updated averageEnergyConsumption
        foreach (var lightingController in lightingControllers)
        {
            if (lightingController != null)
            {
                
                lightingController.UpdateLightingColor(averageEnergyConsumption);
            }
            else
            {
                Debug.LogError("LightingController reference is null.");
            }
        }
    }
}
