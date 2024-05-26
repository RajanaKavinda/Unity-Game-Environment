using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class EnergyStatusController : MonoBehaviour
{
    
    [SerializeField]
 
    private float lastEnergyReading;
    private float currentEnergyReading;
    private DateTime lastTime ; 
    private long tenSecondPeriods;
    private string jwtToken = GetMethod.jwtToken;
    private string urlExtension1 = "/power-consumption/current/view";
    public float averageEnergyConsumption;
    public int fruitsOrFlowers = 0;


    private void Start()
    {
        // Start the coroutine to update power consumption
        StartCoroutine(UpdatePowerConsumption());
    }


    IEnumerator UpdatePowerConsumption()
    {
        // Create an instance of the HttpRequest class
        HttpRequest httpRequest = new HttpRequest();

        while (true)
        {
            if (lastTime!=DateTime.MinValue)
            {
                DateTime currentTime = DateTime.Now;

                // Send HTTP GET request
                yield return StartCoroutine(httpRequest.SendHttpRequest("get", "givenBackend", urlExtension1, jwtToken, ""));

                // Check if the request was successful
                string result = (string)httpRequest.result;
                if (result == "Error")
                {
                    Debug.LogError("Error checking profile and questionnaire");
                    yield break;
                }

                // Deserialize the JSON string into a JsonData object
                CurrentConsumptionResponse jsonData = JsonUtility.FromJson<CurrentConsumptionResponse>(result);

                currentEnergyReading = jsonData.currentConsumption; 
                Debug.LogError("currentEnergyReading"+currentEnergyReading);

                // Calculate the time difference between the current and previous readings
                TimeSpan timeDifference = currentTime - lastTime;

                // Convert the time difference to seconds
                float timeDifferenceInSeconds = (float)timeDifference.TotalSeconds;

                // Calculate the average energy consumption for 10 s
                float energyConsumptionFromLastTime = currentEnergyReading - lastEnergyReading;
                averageEnergyConsumption = energyConsumptionFromLastTime * 10 / timeDifferenceInSeconds;
                updateFruitsOrFlowers(averageEnergyConsumption);
                Debug.Log("averageEnergyConsumption: " + averageEnergyConsumption);

                lastTime = currentTime;
                lastEnergyReading = currentEnergyReading;

                // Wait for 10 seconds before the next update
                yield return new WaitForSeconds(10f);

            }
            else
            {
               Debug.LogError("Last time null.");
               // Send HTTP GET request
               yield return StartCoroutine(httpRequest.SendHttpRequest("get", "givenBackend", urlExtension1, jwtToken, ""));

               // Check if the request was successful
               string result = (string)httpRequest.result;
               if (result == "Error")
               {
                    Debug.LogError("Error checking profile and questionnaire");
                    yield break;
               }

               // Deserialize the JSON string into a JsonData object
               CurrentConsumptionResponse jsonData = JsonUtility.FromJson<CurrentConsumptionResponse>(result);

               lastEnergyReading = jsonData.currentConsumption;
               Debug.LogError("lastEnergyReading"+lastEnergyReading);

               //lastEnergyReading = jsonData.currentConsumption; 
               lastTime = DateTime.Now;
               

               
            }  
        }
    }

    public class CurrentConsumptionResponse
    {
        public float currentConsumption;
       
    }

    

    public void updateFruitsOrFlowers(float averageEnergyConsumption){
        if (averageEnergyConsumption<0.1){
            fruitsOrFlowers += 5;
        } else if (averageEnergyConsumption<0.4){
            fruitsOrFlowers += 3;
        } else if (averageEnergyConsumption<0.8){
            fruitsOrFlowers += 1;
        } else if (averageEnergyConsumption<0.9){
            fruitsOrFlowers -= 1;
        } else if (averageEnergyConsumption<1){
            fruitsOrFlowers -= 3;
        }else{
            fruitsOrFlowers -= 5;
        }
        Debug.LogError("total: "+fruitsOrFlowers);
    }
}