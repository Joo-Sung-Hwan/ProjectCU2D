using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Auth;
using System.Threading.Tasks;
using System;
using TMPro;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using Google;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoogleLoginTest : MonoBehaviour
{
    private FirebaseAuth auth; // 인증에 관한 정보 저장할 객체
    private FirebaseUser user; // 파이어베이스 유저의 정보를 담을 객체

    private string authCode; // 로그인을 위한 유저코드


    void Start()
    {
        // 구글플레이 로그인
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(success =>
        {
            if (success == SignInStatus.Success)
            {
                // RequestServerSideAccess : ServerAuthCode(= code) 를 반환해주는 함수 
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    authCode = code;

                    // 위에서 받은 코드를 바탕으로 로그인 인증서를 발급받기 (GetCredential)
                    auth = FirebaseAuth.DefaultInstance;
                    Credential credential = PlayGamesAuthProvider.GetCredential(authCode);

                    auth.SignInAndRetrieveDataWithCredentialAsync(credential)
                        .ContinueWithOnMainThread(task =>
                        {
                            if (task.IsCompleted)
                            {
                                Debug.Log("로그인 성공 @#$!@$#@@@@@@@@@@@@@@@@@@@@@@@");
                            }

                            Firebase.Auth.AuthResult result = task.Result;
                        });
                });
            }
        });
    }

}