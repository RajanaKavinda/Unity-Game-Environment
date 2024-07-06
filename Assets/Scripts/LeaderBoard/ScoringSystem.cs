using UnityEngine;

public static class ScoringSystem
{
    // Constants for the weights of gems, lands, and coins
    private const int GEM_WEIGHT = 5;
    private const int LAND_WEIGHT = 100;
    private const int COIN_WEIGHT = 1;

    // Calculate the score based on gem count, land count, and coin count
    public static int CalculateScore(int gemCount, int landCount, int coinCount)
    {
        return (gemCount * GEM_WEIGHT) + (landCount * LAND_WEIGHT) + (coinCount * COIN_WEIGHT);
    }
}
