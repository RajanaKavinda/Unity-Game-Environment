using UnityEngine;

public static class ScoringSystem
{
    private const int GEM_WEIGHT = 5;
    private const int LAND_WEIGHT = 100;
    private const int COIN_WEIGHT = 1;

    public static int CalculateScore(int gemCount, int landCount, int coinCount)
    {
        return (gemCount * GEM_WEIGHT) + (landCount * LAND_WEIGHT) + (coinCount * COIN_WEIGHT);
    }
}


