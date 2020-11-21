using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class LoginCheck : MonoBehaviour
{
    public Text myLog;
    public RawImage myImage;
    public Button loginButton;
    public Button logoutButton;

    private bool bWaitingForAuth = false;

    // 로그인 되어있는지 체크
    public void Start()
    {
        if (Social.localUser.authenticated)
        {
            loginButton.gameObject.SetActive(true);
        }
        else
        {
            PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().RequestIdToken().RequestEmail().Build());
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();

            logoutButton.gameObject.SetActive(true);
        }
    }

    // 수동 로그인
    public void OnBtnLoginClicked()
    {
        if (Social.localUser.authenticated)
        {
            Debug.Log(Social.localUser.userName);
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log(Social.localUser.userName);
                    myLog.text = "name : " + Social.localUser.userName + "\n";
                }
                else
                {
                    Debug.Log("Login Fail");
                }
            });
        }
    }

    // 수동 로그아웃
    public void OnBtnLogoutClicked()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }

    // 인증 콜백
    void AuthenticateCallback(bool success)
    {
        if (success)
        {
            StartCoroutine(UserPictureLoad());
        }
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

        myImage.texture = pic;
    }
}
