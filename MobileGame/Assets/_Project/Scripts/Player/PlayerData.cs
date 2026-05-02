using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static int score;
    public static int scoreRecord;

    public static void AddScore(int scoreAdded)
    {
        score += scoreAdded;
    }

    public static void ResetScore()
    {
        score = 0;
    }

    public static void CheckScoreRecord()
    {
        if (score > scoreRecord) scoreRecord = score;
    }
}
