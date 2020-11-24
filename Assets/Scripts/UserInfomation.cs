using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class UserInfomation : MonoBehaviour
{
    //public 
    public Text userId;
    public RawImage userImage;
    public Text userDepth; // 유저의 최대 달성 깊이
    public Text userAchieve; // 유저의 업적 개수
    public Text userUnlock; // 유저가 언락한 지역의 개수

    void Start()
    {
        // 구글 로그인이 되어있다면 유저 아이디, 이미지 가지고오기.
        if (Social.localUser.authenticated)
        {
            userId.text = Social.localUser.userName;
            StartCoroutine(UserPictureLoad());
        }
        else
        {
            userId.text = "Guest";
        }

        userDepth.text = $"{PlayerPrefs.GetInt("maxDepth")} M";
        userAchieve.text = $" 개";
        userUnlock.text = $" 개";
    }

    // 유저 이미지 받아오기
    IEnumerator UserPictureLoad()
    {
        // 최초 유저 이미지
        Texture2D pic = Social.localUser.image;

        // 구글 아바타 이미지 여부를 확인 후 이미지 생성
        while (pic == null)
        {
            pic = Social.localUser.image;
            yield return null;
        }

        userImage.texture = pic;
    }
}