using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;

public class TestLogin : MonoBehaviour
{
    public Text log;

    FirebaseAuth auth;
    FirebaseUser user;

    string email = string.Empty;
    string password = string.Empty;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        email = $"b01077016039@gmail.com";
        password = $"admin1234";

        //CreateUser();

        auth
      .SignInAnonymouslyAsync()
      .ContinueWith(task => {
          if (task.IsCanceled)
          {
              Debug.LogError("SignInAnonymouslyAsync was canceled.");
              return;
          }
          if (task.IsFaulted)
          {
              Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
              return;
          }

          user = task.Result;
          log.text = "성공";
          Debug.Log(string.Format("User signed in successfully: {0} ({1})",
              user.DisplayName, user.UserId));
      });

        //auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        //{
        //    if (task.IsCanceled)
        //    {
        //        Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
        //        return;
        //    }
        //    if (task.IsFaulted)
        //    {
        //        Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
        //        Debug.Log("개병신 파이어베이스 연동도 안됨 ㅋㅋ");
        //        return;
        //    }

        //    FirebaseUser newUser = task.Result;
        //    Debug.LogFormat("User signed in successfully: {0} ({1})",
        //        newUser.DisplayName, newUser.UserId);

        //});
    }

    void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                Debug.Log("씨발 좆같은거 연동도 안됨 ㅋㅋ");
                return;
                
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
}
