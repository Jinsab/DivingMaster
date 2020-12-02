using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class UserAchieve : MonoBehaviour
{
    private int depth; // 플레이어 최대 도달 깊이
    private int data = 0; // 업적 갯수

    // Start is called before the first frame update
    void Start()
    {
        depth = PlayerPrefs.GetInt("maxDepth");

        // 업적 달성은 2부터 시작
        if (depth >= 200)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_200_meters_over, 2, null);
            data++;
        }

        if (depth >= 500)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_500_meters_over, 3, null);
            data++;
        }

        if (depth >= 800)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_800_meters_over, 4, null);
            data++;
        }

        if (depth >= 1300)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_1300_meters_over, 5, null);
            data++;
        }

        if (depth >= 2000)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_2000_meters_over, 6, null);
            data++;
        }

        if (depth >= 3600)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_3600_meters_over, 7, null);
            data++;
        }

        if (depth >= 6200)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_6200_meters_over, 8, null);
            data++;
        }

        if (depth >= 7800)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_7800_meters_over, 9, null);
            data++;
        }

        if (depth >= 11000)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_11000_meters_over, 10, null);
            data++;
        }
    }

    public int getData()
    {
        return data;
    }
}
