using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtLogin;
    [SerializeField] private TextMeshProUGUI txtNickname;
    [SerializeField] private TMP_InputField inpNickname;
    [SerializeField] private GameObject createNickname;
    [SerializeField] private GameObject dlgNickname;
    [SerializeField] private GameObject dlgLoginFailed;
    [SerializeField] private GameObject dlgInvalidNickname;
    [SerializeField] private Button btnCreateDlgNickname;
    [SerializeField] private Button btnCreateNickname;
    [SerializeField] private Button btnExitCreateNickname;
    [SerializeField] private Button btnExitInvalidNickname;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnStart;

    public TMP_InputField InpNickname => inpNickname;


    private void Start()
    {
        btnCreateDlgNickname.onClick.AddListener(OnCreateDialogNickname);
        btnCreateNickname.onClick.AddListener(OnCreateNickname);
        btnExitCreateNickname.onClick.AddListener(OnExitCreateNickname);
        btnExitInvalidNickname.onClick.AddListener(OnExitInvalidNickname);
        btnStart.onClick.AddListener(LoadStartScene);
        btnExit.onClick.AddListener(OnExit);
    }


    public void SetCreateNicknameUI()
    {
        Debug.Log("LoginUIController - SetCreateNicknameUI @@@@@@@@@@@@@@@@@");
        inpNickname.text = "";
        txtLogin.gameObject.SetActive(false);
        createNickname.SetActive(true);
    }

    public void StartGame()
    {
        btnStart.gameObject.SetActive(true);
        txtLogin.text = "Touch To Start";
    }

    public void LoginFailed()
        => dlgLoginFailed.gameObject.SetActive(true);


    private void OnCreateDialogNickname() // 닉네임 생성 버튼에 등록
    {
        if (Regex.IsMatch(inpNickname.text, Settings.regex) == false)
        {
            dlgInvalidNickname.SetActive(true);
            return;
        }

        dlgNickname.SetActive(true);

        txtNickname.text = $"Use [{inpNickname.text}] ?";
    }

    private void OnCreateNickname() // 닉네임 확정 버튼에 등록
    {
        dlgNickname.SetActive(false);
        createNickname.SetActive(false);
        txtLogin.gameObject.SetActive(true);

        StartGame();
    }

    private void OnExitInvalidNickname() // 닉네임 재생성 버튼에 등록
        => dlgInvalidNickname.SetActive(false);

    private void OnExitCreateNickname() // 다이얼로그 취소 버튼에 등록
        => dlgNickname.SetActive(false);

    private void OnExit()
        => Application.Quit();

    public void LoadStartScene() // 스타트 버튼에 등록
    {
        // 최초 실행에 필요한 리소스 로드 (어드레서블 활용)
        //List<string> levelResources = new List<string> { "Database", "Sprites", "Prefabs" };
        List<string> levelResources = new List<string> {};
        LoadingSceneManager.LoadScene("TestMainMenuScene", "TestA", ESceneType.MainMenu);
    }
}
