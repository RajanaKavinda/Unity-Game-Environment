using System;
using System.Collections.Generic;
using UnityEngine;

public class GemsManager : MonoBehaviour
{
    // Singleton instance of GemsManager
    public static GemsManager Instance { get; private set; }
    public int totalGems = 0; // Total gems accumulated

    private void Awake()
    {
        // Ensure only one instance of GemsManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    public void UpdateGems()
    {
        DateTime currentDate = DateTime.Now.Date; // Get current date
        string lastPlayedDateStr = PlayerPrefs.GetString("LastPlayedDate", currentDate.ToString()); // Get last played date from PlayerPrefs
        Debug.Log("Last played date: " + lastPlayedDateStr);
        DateTime lastPlayedDate;

        // Parse the last played date string; if invalid, default to yesterday
        if (!DateTime.TryParse(lastPlayedDateStr, out lastPlayedDate))
        {
            lastPlayedDate = DateTime.Now.AddDays(-1);
        }

        // Fetch data from the last played date to the day before the current date
        DateTime dateToFetch = lastPlayedDate.Date;
        while (dateToFetch < currentDate)
        {
            DataFetcher.Instance.FetchData(dateToFetch.Year, dateToFetch.ToString("MMMM").ToUpper()); // Fetch data for each month
            dateToFetch = dateToFetch.AddMonths(1); // Move to the next month
        }
    }

    public void CalculateAndAccumulateGems(PowerConsumptionResponse response)
    {
        // Validate the response object
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

        DateTime today = DateTime.Now.Date; // Get today's date
        DateTime lastPlayedDate = DateTime.Parse(PlayerPrefs.GetString("LastPlayedDate")).Date; // Get the last played date from PlayerPrefs
        int currentMonth = response.dailyPowerConsumptionView.month; // Get current month from response
        int currentYear = response.dailyPowerConsumptionView.year; // Get current year from response

        // Iterate through each day of the month
        for (int day = 1; day <= DateTime.DaysInMonth(currentYear, currentMonth); day++)
        {
            DateTime date = new DateTime(currentYear, currentMonth, day); // Create a date object for each day

            // Only process dates between the last played date and yesterday
            if (date < lastPlayedDate || date >= today)
            {
                continue;
            }

            string dayStr = day.ToString();
            if (dailyUnits.ContainsKey(dayStr))
            {
                float units = dailyUnits[dayStr]; // Get power consumption units for the day
                int n = SaveManager.Instance.GetPlacedItemCount("Tree"); // Get the count of placed trees
                Debug.Log("Number of trees: " + n + " for day " + day);
                int gems = 0;

                // Calculate gems based on power consumption units
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

                totalGems += gems; // Accumulate gems
            }
        }

        PlayerPrefs.SetInt("TotalGems", totalGems); // Save total gems to PlayerPrefs
        PlayerPrefs.Save();
        Debug.Log("Total Gems after calculation: " + totalGems);

        // Update gems display
        GemsDisplay.Instance.UpdateGemsDisplay();

        // Save the current date as the last played date
        PlayerPrefs.SetString("LastPlayedDate", DateTime.Now.ToString("yyyy-MM-dd"));
    }
}
