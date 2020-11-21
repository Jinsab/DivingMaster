using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Google;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Auth;
using Firebase.Extensions;

public class GoogleLoginManager : MonoBehaviour
{
    /*
     * 빌드시 admob androidmanifest에 쳐넣기
     *       <!-- Your AdMob app ID will look similar to this
          sample ID: ca-app-pub-3940256099942544~3347511713
          addmob ID: ca-app-pub-8494318270906763/4689123271-->

      <meta-data android:name="com.google.android.gms.ads.APPLICATION_ID"
          android:value="ca-app-pub-3940256099942544~3347511713"/>

     */
    public Text myLog;
    public GameObject startPanel;

    private bool bWaitingForAuth = false;
    private bool signedIn = false;
    private FirebaseAuth auth; // auth용 instance
    private FirebaseUser user; // 사용자

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("연동");
            }
            else
            {
                Debug.LogError("모든 Firebase 종속성을 해결할 수 없습니다." + dependencyStatus);
            }
        });

        myLog.text = "로그인 준비중...";
    }

    // 출처: https://seonbicode.tistory.com/45 [코드저장소]
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            // 연동된 계정과 기기의 계정이 같다면 true를 리턴한다.
            signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            
            user = auth.CurrentUser;
            
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    // 완성
    public void GuestLogin()
    {
        // 익명 로그인 진행
        myLog.text = "연결 중...";
        //Debug.Log("진행중");

        //auth.SignInAnonymouslyAsync().ContinueWith(task => {
        //    if (task.IsCanceled)
        //    {
        //        Debug.LogError("SignInAnonymouslyAsync was canceled.");
        //        return;
        //    }
        //    if (task.IsFaulted)
        //    {
        //        Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
        //        return;
        //    }

        //    user = task.Result;
        //    Debug.LogFormat("User signed in successfully: {0} ({1})",
        //        user.DisplayName, user.UserId);
        //});

        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                StartCoroutine("LoadingGame");
                return;
            }

            // 익명 로그인 연동 결과

            user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);
        });

        StartCoroutine("LoadingGame");
    }

    // 출처: https://seonbicode.tistory.com/45 [코드저장소]
    /*
    public void EmailLogin()
    {
        // 적당한 UGUI 를 만들어 email, pw 를 입력받는다.
        var email = EmailCreatePanel.transform.Find("email").Find("Text").GetComponent<Text>().text;
        var pw = EmailCreatePanel.transform.Find("pw").Find("Text").GetComponent<Text>().text;
        
        if (email.Length < 1 || pw.Length < 1)
        {
            Debug.Log("이메일 ID 나 PW 가 비어있습니다.");
            return;
        }
        
        auth.CreateUserWithEmailAndPasswordAsync(email, pw).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            
            // firebase email user create
            
            FirebaseUser newUser = task.Result;
            
            Debug.LogFormat("Firebase Email user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            return;
        });
    }
    */

    public void GoogleLogin()
    {
        if (GoogleSignIn.Configuration == null)
        {
            // 설정
            GoogleSignIn.Configuration = new GoogleSignInConfiguration
            {
                RequestIdToken = true,
                RequestEmail = true,
                // Copy this value from the google-service.json file.
                // oauth_client with type == 3
                WebClientId = "166022739614-1162j1l9mpus665rtrjcq1tplnnai8lh.apps.googleusercontent.com"
            };
        }

        Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();
        TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser>();

        signIn.ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Google Login task.IsCanceled");
                myLog.text = "구글 로그인 실패 : 캔슬";
            }
            else if (task.IsFaulted)
            {
                Debug.Log("Google Login task.IsFaulted");
                myLog.text = "구글 로그인 실패 : 오류";
            }
            else
            {
                Credential credential =
                    GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)task).Result.IdToken, null);
                
                auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
                {
                    if (authTask.IsCanceled)
                    {
                        signInCompleted.SetCanceled();
                        Debug.Log("Google Login authTask.IsCanceled");
                        myLog.text = "Auth 로그인 실패 : 캔슬";
                        return;
                    }
                    if
                    (authTask.IsFaulted)
                    {
                        signInCompleted.SetException(authTask.Exception);
                        Debug.Log("Google Login authTask.IsFaulted");
                        myLog.text = "Auth 로그인 실패 : 오류";
                        return;
                    }
                    
                    user = authTask.Result;
                    Debug.LogFormat("Google User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);
                    //PlayStoreLogin();

                    return;
                });
            }
        });
    }

    public void PlayStoreLogin()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().RequestIdToken().RequestEmail().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        myLog.text = "...";

        if (bWaitingForAuth)
        {
            myLog.text = "로그인 되었음";
            return;
        }

        // 구글 로그인이 되어있지 않다면
        if (!Social.localUser.authenticated)
        {
            myLog.text = "연결 중...";
            bWaitingForAuth = true;

            // 로그인 인증 처리과정 (콜백함수)
            Social.localUser.Authenticate(AuthenticateCallback);

            StartCoroutine(TryFirebaseGoogleLogin());
        }
        else
        {
            myLog.text = "로그인 실패";
        }
    }

    // 인증 콜백
    void AuthenticateCallback(bool success)
    {
        myLog.text = "연결완료";

        if (success)
        {
            myLog.text = $"환영합니다.\n{Social.localUser.userName} 님";
        }
        else
        {
            myLog.text = "로그인 실패";
        }
    }

    IEnumerator TryFirebaseGoogleLogin()
    {
        while (string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
        {
            yield return null;
        }
            
        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                myLog.text = "실패";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                myLog.text = "오류";
                StartCoroutine("LoadingGame");
                return;
            }

            user = auth.CurrentUser;
            StartCoroutine("LoadingGame");

            Debug.Log("Success!");
        });
    }

    IEnumerator LoadingGame()
    {
        myLog.text = "게임 준비완료...";

        startPanel.SetActive(true);

        while (true)
        {
            if (!startPanel.activeSelf)
            {
                SceneManager.LoadScene(1);
            }

            yield return null;
        }
    }
}
