using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

public class DataFetcher : MonoBehaviour
{
    public static DataFetcher Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FetchData(int year, string month)
    {
        StartCoroutine(FetchDataCoroutine(year, month));
    }

    private IEnumerator FetchDataCoroutine(int year, string month)
    {
        string url = $"http://20.15.114.131:8080/api/power-consumption/month/daily/view?year={year}&month={month}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", "Bearer " + GetMethod.jwtToken);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to fetch data: " + request.error);
            Debug.LogError("HTTP Status Code: " + request.responseCode);

            if (request.responseCode == 500)
            {
                Debug.LogError("Server error. Please try again later.");
            }
        }
        else
        {
            ProcessData(request.downloadHandler.text);
        }
    }

    private void ProcessData(string jsonData)
    {
        try
        {
            Debug.Log("JSON Data: " + jsonData);
            PowerConsumptionResponse response = JsonConvert.DeserializeObject<PowerConsumptionResponse>(jsonData);
            Debug.Log("Deserialized Response: " + JsonConvert.SerializeObject(response, Formatting.Indented));

            if (response.dailyPowerConsumptionView == null)
            {
                Debug.LogError("dailyPowerConsumptionView is null");
                return;
            }

            if (response.dailyPowerConsumptionView.dailyUnits == null)
            {
                Debug.LogError("dailyUnits is null");
                return;
            }

            GemsManager.Instance.CalculateAndAccumulateGems(response);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error processing data: " + e.Message);
        }
    }
}


[System.Serializable]
public class PowerConsumptionResponse
{
    public DailyPowerConsumptionView dailyPowerConsumptionView;
}

[System.Serializable]
public class DailyPowerConsumptionView
{
    public int year;
    public int month;
    public Dictionary<string, float> dailyUnits;
}

