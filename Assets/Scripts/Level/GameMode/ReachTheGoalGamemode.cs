using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTheGoalGamemode : LevelGamemode
{
    [SerializeField]
    private RoundTracker roundTracker;
    private int lowestRoundsAmount, mediumRoundsAmount, highRoundsAmount;

    public ReachTheGoalGamemode(int highScore, int mediumScore, int lowScore)
    {
        UpdateRanks(highScore, mediumScore, lowScore);
    }

    public ReachTheGoalGamemode(string highScore, string mediumScore, string lowScore)
    {
        int.TryParse(highScore, out int highScoreInt);
        int.TryParse(mediumScore, out int mediumScoreInt);
        int.TryParse(lowScore, out int lowScoreInt);
        UpdateRanks(highScoreInt, mediumScoreInt, lowScoreInt);
    }

    private void UpdateRanks(int highScore, int mediumScore, int lowScore)
    {
        lowestRoundsAmount = highScore;
        mediumRoundsAmount = mediumScore;
        highRoundsAmount = lowScore;
    }

    public override int GetRating()
    {
        if (roundTracker.Round <= lowestRoundsAmount) return 3;
        if (roundTracker.Round <= mediumRoundsAmount) return 2;
        if (roundTracker.Round <= highRoundsAmount) return 1;
        return 0;
    }
}
