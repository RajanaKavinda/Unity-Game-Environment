using System;
using System.Collections.Generic;
using UnityEngine;

public class GemsManager : MonoBehaviour
{
    public static GemsManager Instance { get; private set; }
    public int totalGems = 0;

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

    public void UpdateGems()
    {
        DateTime currentDate = DateTime.Now.Date;
        string lastPlayedDateStr = PlayerPrefs.GetString("LastPlayedDate", currentDate.ToString());
        Debug.Log("Last played date: " + lastPlayedDateStr);
        DateTime lastPlayedDate;

        if (!DateTime.TryParse(lastPlayedDateStr, out lastPlayedDate))
        {
            lastPlayedDate = DateTime.Now.AddDays(-1); // Default to yesterday if no last played date
        }

        // Fetch data from the last played date up to the day before the current date
        DateTime dateToFetch = lastPlayedDate.Date;
        while (dateToFetch < currentDate)
        {
            DataFetcher.Instance.FetchData(dateToFetch.Year, dateToFetch.ToString("MMMM").ToUpper());
            dateToFetch = dateToFetch.AddMonths(1);
        }
    }

    public void CalculateAndAccumulateGems(PowerConsumptionResponse response)
    {
        if (response.dailyPowerConsumptionView == null)
        {
            Debug.LogError("dailyPowerConsumptionView is null");
            return;
        }

        Dictionary<string, float> dailyUnits = response.dailyPowerConsumptionView.dailyUnits;
        if (dailyUnits == null)
        {
            Debug.LogError("dailyUnits is null");
            return;
        }

        DateTime today = DateTime.Now.Date;
        DateTime lastPlayedDate = DateTime.Parse(PlayerPrefs.GetString("LastPlayedDate")).Date;
        int currentMonth = response.dailyPowerConsumptionView.month;
        int currentYear = response.dailyPowerConsumptionView.year;

        for (int day = 1; day <= DateTime.DaysInMonth(currentYear, currentMonth); day++)
        {
            DateTime date = new DateTime(currentYear, currentMonth, day);

            // Only consider days between the last played date and yesterday
            if (date < lastPlayedDate || date >= today)
            {
                continue;
            }

            string dayStr = day.ToString();
            if (dailyUnits.ContainsKey(dayStr))
            {
                float units = dailyUnits[dayStr];
                int n = SaveManager.Instance.GetPlacedItemCount("Tree");
                Debug.Log("Number of trees: " + n + " for day " + day);
                int gems = 0;

                if (units <= 3)
                {
                    gems = 3 * (2 + n);
                }
                else if (units <= 6)
                {
                    gems = 2 * (2 + n);
                }
                else if (units <= 9)
                {
                    gems = 1 * (2 + n);
                }

                totalGems += gems;
            }
        }

        PlayerPrefs.SetInt("TotalGems", totalGems);
        PlayerPrefs.Save();
        Debug.Log("Total Gems after calculation: " + totalGems);

        // Update gems display
        GemsDisplay.Instance.UpdateGemsDisplay();

        // Save the current date as the last played date
        PlayerPrefs.SetString("LastPlayedDate", DateTime.Now.ToString("yyyy-MM-dd"));
    }
}

