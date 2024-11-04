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
using Firebase;

public class GoogleLoginTest : MonoBehaviour
{
    [SerializeField] private LoginUIController loginUIController;

    private FirebaseAuth auth; // ������ ���� ���� ������ ��ü
    private FirebaseUser user; // ���̾�̽� ������ ������ ���� ��ü

    private string authCode; // �α����� ���� �����ڵ�


    void Start()
    {
        // �����÷��� �α���
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(success =>
        {
            if (success == SignInStatus.Success)
            {
                // RequestServerSideAccess : ServerAuthCode(= code) �� ��ȯ���ִ� �Լ� 
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    authCode = code;

                    // ������ ���� �ڵ带 �������� �α��� �������� �߱޹ޱ� (GetCredential)
                    auth = FirebaseAuth.DefaultInstance;
                    Credential credential = PlayGamesAuthProvider.GetCredential(authCode);

                    auth.SignInAndRetrieveDataWithCredentialAsync(credential)
                        .ContinueWithOnMainThread(task =>
                        {
                            if (task.IsCompleted)
                            {
                                Debug.Log("GoogleLoginTest - Start @@@@@@@@@@@@@@@@@");

                                HasNicknameByID(); // �α��ο� �����ϸ� ���� �ִ��� �˻�
                            }

                            Firebase.Auth.AuthResult result = task.Result;
                        });
                });
            }
        });
    }

    private void HasNicknameByID()
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            Debug.LogError("Firebase not initialized!");
            return;
        }

        // ���� �α����� ���̾�̽� ������ UserId�� �����ͼ� Nickname �����Ͱ� �ִ��� �˻�
        user = auth.CurrentUser;
        DatabaseReference nameDB = FirebaseDatabase.DefaultInstance.GetReference("Nickname");

        Debug.Log($"nameDB is null? : {(nameDB == null)} ");

        nameDB.OrderByKey().EqualTo(user.UserId).GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                // ���� ó��
                Debug.Assert(task.Exception != null, "Task Exception!!" + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("GoogleLoginTest - HasNicknameByID $#####################");

                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists) // �����Ͱ� �����ϴ� ��� (Exists = �����ϴ�)
                {
                    foreach (var childSnapshot in snapshot.Children)
                    {
                        // �г��� ���ÿ� ���� (�ٸ� �������� ������ ���)
                        string nickname = childSnapshot.Value.ToString();
                        PlayerPrefs.SetString("Nickname", nickname);
                        PlayerPrefs.Save();

                        loginUIController.StartGame();
                        break; // ù ��° (�׸��� ������) �ڽĸ� ó���մϴ�.
                    }
                }
                else
                {
                    // �����Ͱ� �������� �ʴ� ���
                    Debug.Log("GoogleLoginTest - HasNicknameByID @@@@@@@@@@@@@@@@@");

                    loginUIController.SetCreateNicknameUI();

                }
            }
            else
            {
                Debug.Log("GoogleLoginTest - "+task.Status);
            }
        });
    }

    public void CreateNickname() // ���̾�α� ��ư�� ���
    {
        DatabaseReference nameDB = FirebaseDatabase.DefaultInstance.GetReference("Nickname");

        // <�������̵�, �����г���> ��ųʸ� : ������ ID���� ������ �г��� ��������� ����
        Dictionary<string, object> nicknameDic = new Dictionary<string, object>();

        // ���Խ� �߰� (regex)
        string nickname = loginUIController.InpNickname.text;
        nicknameDic.Add(user.UserId, nickname);

        // nameDB�� nicknameDic�� �߰��ؼ� ������ ������Ʈ
        nameDB.UpdateChildrenAsync(nicknameDic).ContinueWithOnMainThread(task =>
        {
            //if (task.IsFaulted || task.IsCanceled) dlgNickname.SetActive(false);

            if (task.IsCompleted)
            {
                Debug.Log("GoogleLoginTest - CreateNickname @@@@@@@@@@@@@@@@@");
                PlayerPrefs.SetString("Nickname", nickname);
                PlayerPrefs.Save();
            }
        });
    }
}