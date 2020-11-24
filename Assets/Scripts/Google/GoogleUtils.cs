using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GoogleUtils : MonoBehaviour
{
    public void OnAchievementShow()
    {
        Social.ShowAchievementsUI();
    }

    public void OnLeaderboardShow()
    {
        Social.ShowLeaderboardUI();
    }
}
