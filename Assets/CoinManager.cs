using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static int currentCoins = 1000; // Initial number of coins

    // Function to decrease coins count
    public static void DecreaseCoins(int amount)
    {
        currentCoins -= amount;
    }

    // Function to increase coins count
    public static void IncreaseCoins(int amount)
    {
        currentCoins += amount;
    }
}
