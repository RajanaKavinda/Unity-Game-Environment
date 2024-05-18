using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static int currentCoins = 0; // Initial number of coins

    // Function to decrease coins count
    public static void DecreaseCoins(int amount)
    {
        currentCoins = Mathf.Max(0, currentCoins - amount);
    }

    // Function to increase coins count
    public static void IncreaseCoins(int amount)
    {
        currentCoins += amount;
    }

    public static void SetCoins(int amount)
    {
        currentCoins = amount;
    }

}
