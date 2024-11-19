using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Photon Unity Network
using Photon.Realtime;
using UnityEngine.SceneManagement; // �ǽð� ó���� ���� ���̺귯��

public class PhotonManager : MonoBehaviourPunCallbacks
{
    string gameVersion = "0.0.1";
    int maxPlayer = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        // OnSettingNickName(PlayerPrefs.GetString("Nickname"));
        OnSettingNickName("123");
        Debug.Log(PhotonNetwork.NickName); // �г��� �׽�Ʈ
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ���� ���� �Լ�
    /// </summary>
    public void Connect()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            Debug.Log("�г��� ����");
            return;
        }
        // ������ �Ǿ��� ��
        if (PhotonNetwork.IsConnected)
        {
            // PhotonNetwork.JoinRandomRoom();
            Debug.Log("����");
        }
        else
        {
            // ���ӹ����� ��������� ���߰� ����
            // Debug.LogFormat("Connect : {0}", gameVersion);
            // PhotonNetwork.GameVersion = gameVersion;
            // OnConnectedToMaster ȣ��
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void OnSettingNickName(string name)
    {
        PhotonNetwork.NickName = name;
    }

    /// <summary>
    /// ���� �������� ��
    /// </summary>
    public override void OnConnected()
    {
        Debug.Log("���� ����");
        // �κ� ������ �ؾ� �濡 ���� ����
        // PhotonNetwork.JoinLobby();
    }

    /// <summary>
    /// ������ ������ ����
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.LogFormat("Connected to Master: {0}", PhotonNetwork.NickName);
        PhotonNetwork.JoinRandomOrCreateRoom(expectedMaxPlayers: 2, roomOptions: new RoomOptions() { MaxPlayers = 2 });
    }

    /// <summary>
    /// ���� ������ ��
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Disconnected: {0}", cause);
    }

    /// <summary>
    /// �κ� ����
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ����");
    }
    /// <summary>
    /// �� ���� �� �ڵ����� ȣ��
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom);
        // �����÷��� �� ȣ��, �ε��
        // PhotonNetwork.LoadLevel("");
        if(PhotonNetwork.CountOfPlayersInRooms == 2)
        {
            LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);
        }
        else
        {
            Debug.Log("1���� �����մϴ�.");
            LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);
        }
    }

    /// <summary>
    /// ���� ��Ī(�� ����) ����
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("JoinRandomFailed({0}): {1}", returnCode, message);
    }


}

