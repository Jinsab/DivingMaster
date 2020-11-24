using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class LoginCheck : MonoBehaviour
{
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
            //Debug.Log(Social.localUser.userName);
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    //Debug.Log(Social.localUser.userName);

                    loginButton.gameObject.SetActive(true);
                    logoutButton.gameObject.SetActive(false);
                }
                else
                {
                    //Debug.Log("Login Fail");
                }
            });
        }
    }

    // 수동 로그아웃
    public void OnBtnLogoutClicked()
    {
        logoutButton.gameObject.SetActive(true);
        loginButton.gameObject.SetActive(false);

        ((PlayGamesPlatform)Social.Active).SignOut();
    }

    // 인증 콜백
    void AuthenticateCallback(bool success)
    {
        if (success)
        {
            //Debug.Log("연결 성공!");
        }
    }
}
