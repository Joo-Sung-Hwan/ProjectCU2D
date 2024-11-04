using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtLogin;
    [SerializeField] private TextMeshProUGUI txtNickname;
    [SerializeField] private TMP_InputField inpNickname;
    [SerializeField] private GameObject createNickname;
    [SerializeField] private GameObject dlgNickname;
    [SerializeField] private Button btnCreateDlgNickname;
    [SerializeField] private Button btnCreateNickname;
    [SerializeField] private Button btnExitCreateNickname;
    [SerializeField] private Button btnStart;

    public TMP_InputField InpNickname => inpNickname;


    private void Start()
    {
        btnCreateDlgNickname.onClick.AddListener(OnCreateDialogNickname);
        btnCreateNickname.onClick.AddListener(OnCreateNickname);
        btnExitCreateNickname.onClick.AddListener(ExitCreateNickname);
        btnStart.onClick.AddListener(LoadStartScene);
    }


    public void SetCreateNicknameUI()
    {
        Debug.Log("LoginUIController - SetCreateNicknameUI @@@@@@@@@@@@@@@@@");
        txtLogin.gameObject.SetActive(false);
        createNickname.SetActive(true);
    }


    public void StartGame()
    {
        btnStart.gameObject.SetActive(true);
        txtLogin.text = "Touch To Start";
    }

    private void OnCreateDialogNickname() // Confirm ��ư�� ���
    {
        if (inpNickname.text == "") return;

        dlgNickname.SetActive(true);

        txtNickname.text = $"Use [{inpNickname.text}] ?";
    }

    private void OnCreateNickname() // ���̾�α� ��ư�� ���
    {
        dlgNickname.SetActive(false);
        createNickname.SetActive(false);
        txtLogin.gameObject.SetActive(true);

        StartGame();
    }

    private void ExitCreateNickname() // ���̾�α� ��� ��ư�� ���
    {
        dlgNickname.SetActive(false);
    }

    public void LoadStartScene() // ��ŸƮ ��ư�� ���
    {
        // ���� ���࿡ �ʿ��� ���ҽ� �ε� (��巹���� Ȱ��)
        //List<string> levelResources = new List<string> { "Database", "Sprites", "Prefabs" };
        //LoadingSceneManager.LoadScene("MainScene", levelResources);
    }
}
