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
    private DateTime lastTime ; // May 12, 2024, 6:28:00 PM; = new DateTime(2024, 5, 12, 18, 28, 0)
    private long tenSecondPeriods;
    private string jwtToken = GetMethod.jwtToken;
    private string urlExtension1 = "/power-consumption/current/view";
    private float averageEnergyConsumption;
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

                if (currentTime.Date == lastTime.Date)
                {
                    Debug.Log("Current date is the same as the last date."+currentTime);

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

                    tenSecondPeriods = (long) timeDifferenceInSeconds/10;
                    if (tenSecondPeriods==0){
                        tenSecondPeriods+=1;
                    }

                    // Print the timestamp
                    Debug.Log("Timestamp: " + currentTime + " , timeDifference " + timeDifference + " timeDifferenceInSeconds " + timeDifferenceInSeconds);

                    // Calculate the average energy consumption for 10 s
                    float energyConsumptionFromLastTime = currentEnergyReading - lastEnergyReading;
                    averageEnergyConsumption = energyConsumptionFromLastTime * 10 / timeDifferenceInSeconds;
                    updateFruits(averageEnergyConsumption);
                    Debug.Log("averageEnergyConsumption: " + averageEnergyConsumption);

                    //updateFruits(averageEnergyConsumption,tenSecondPeriods);

                    

                    lastTime = currentTime;
                    lastEnergyReading = currentEnergyReading;

                    // Wait for 10 seconds before the next update
                    yield return new WaitForSeconds(10f);

                }
                else if (currentTime.Month == lastTime.Month && currentTime.Year == lastTime.Year)
                {
                   Debug.Log("Current date is in the same month as the last date.");
                }
                else
                {
                    Debug.Log("Current date is not in the same month as the last date.");
                }
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

    

    public void updateFruits(float averageEnergyConsumption){
        if (averageEnergyConsumption<0.2){
            fruitsOrFlowers += 5;
        } else if (averageEnergyConsumption<0.4){
            fruitsOrFlowers += 3;
        } else if (averageEnergyConsumption<0.8){
            fruitsOrFlowers += 1;
        } else if (averageEnergyConsumption<1){
            fruitsOrFlowers -= 1;
        } else{
            fruitsOrFlowers -= 3;
        }
        Debug.LogError("totalFruits: "+fruitsOrFlowers);
    }
}