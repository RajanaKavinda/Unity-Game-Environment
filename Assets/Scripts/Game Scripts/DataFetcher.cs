using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

public class DataFetcher : MonoBehaviour
{
    // Singleton instance
    public static DataFetcher Instance { get; private set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one
        }
    }

    // Fetch power consumption data for a specific year and month
    public void FetchData(int year, string month)
    {
        StartCoroutine(FetchDataCoroutine(year, month)); // Start coroutine to fetch data
    }

    // Coroutine to fetch data from the API
    private IEnumerator FetchDataCoroutine(int year, string month)
    {
        string url = $"http://20.15.114.131:8080/api/power-consumption/month/daily/view?year={year}&month={month}";
        UnityWebRequest request = UnityWebRequest.Get(url); // Create a UnityWebRequest object
        request.SetRequestHeader("Authorization", "Bearer " + GetMethod.jwtToken); // Set authorization header

        yield return request.SendWebRequest(); // Send the request and wait for response

        if (request.result != UnityWebRequest.Result.Success) // Check if request was successful
        {
            Debug.LogError("Failed to fetch data: " + request.error); // Log error if request failed
            Debug.LogError("HTTP Status Code: " + request.responseCode); // Log HTTP status code

            if (request.responseCode == 500) // Check if server error
            {
                Debug.LogError("Server error. Please try again later.");
            }
        }
        else // Request successful
        {
            ProcessData(request.downloadHandler.text); // Process the received data
        }
    }

    // Process the received JSON data
    private void ProcessData(string jsonData)
    {
        try
        {
            Debug.Log("JSON Data: " + jsonData); // Log the received JSON data
            PowerConsumptionResponse response = JsonConvert.DeserializeObject<PowerConsumptionResponse>(jsonData); // Deserialize JSON data
            Debug.Log("Deserialized Response: " + JsonConvert.SerializeObject(response, Formatting.Indented)); // Log deserialized response

            if (response.dailyPowerConsumptionView == null) // Check if dailyPowerConsumptionView is null
            {
                Debug.LogError("dailyPowerConsumptionView is null");
                return;
            }

            if (response.dailyPowerConsumptionView.dailyUnits == null) // Check if dailyUnits is null
            {
                Debug.LogError("dailyUnits is null");
                return;
            }

            GemsManager.Instance.CalculateAndAccumulateGems(response); // Call method to calculate and accumulate gems
        }
        catch (System.Exception e) // Catch any exceptions during data processing
        {
            Debug.LogError("Error processing data: " + e.Message); // Log error message
        }
    }
}

// Serializable class to represent the response from the API
[System.Serializable]
public class PowerConsumptionResponse
{
    public DailyPowerConsumptionView dailyPowerConsumptionView; // Contains daily power consumption data
}

// Serializable class to represent daily power consumption data
[System.Serializable]
public class DailyPowerConsumptionView
{
    public int year; // Year of the data
    public int month; // Month of the data
    public Dictionary<string, float> dailyUnits; // Dictionary containing daily power consumption units
}
