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
		int maxDepth = PlayerPrefs.GetInt("maxDepth");

		Social.ReportScore(maxDepth, GPGSIds.leaderboard_maximum_depth, (bool bSuccess) =>
		{
			if (bSuccess)
			{
				Debug.Log("ReportLeaderBoard Success");
			}
			else
			{
				Debug.Log("ReportLeaderBoard Fall");
			}
		});

		Social.ShowLeaderboardUI();
    }
}
