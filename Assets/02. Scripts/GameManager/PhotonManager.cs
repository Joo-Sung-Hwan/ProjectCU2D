using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Photon Unity Network
using Photon.Realtime;
using UnityEngine.SceneManagement; // 실시간 처리를 위한 라이브러리

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
        Debug.Log(PhotonNetwork.NickName); // 닉네임 테스트
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 포톤 연결 함수
    /// </summary>
    public void Connect()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            Debug.Log("닉네임 설정");
            return;
        }
        // 접속이 되었을 때
        if (PhotonNetwork.IsConnected)
        {
            // PhotonNetwork.JoinRandomRoom();
            Debug.Log("접속");
        }
        else
        {
            // 게임버전과 포톤버전을 맞추고 접속
            // Debug.LogFormat("Connect : {0}", gameVersion);
            // PhotonNetwork.GameVersion = gameVersion;
            // OnConnectedToMaster 호출
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void OnSettingNickName(string name)
    {
        PhotonNetwork.NickName = name;
    }

    /// <summary>
    /// 접속 성공했을 때
    /// </summary>
    public override void OnConnected()
    {
        Debug.Log("연결 성공");
        // 로비 입장을 해야 방에 입장 가능
        // PhotonNetwork.JoinLobby();
    }

    /// <summary>
    /// 마스터 서버에 접속
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.LogFormat("Connected to Master: {0}", PhotonNetwork.NickName);
        PhotonNetwork.JoinRandomOrCreateRoom(expectedMaxPlayers: 2, roomOptions: new RoomOptions() { MaxPlayers = 2 });
    }

    /// <summary>
    /// 접속 끊겼을 때
    /// </summary>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Disconnected: {0}", cause);
    }

    /// <summary>
    /// 로비 접속
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("로비 접속");
    }
    /// <summary>
    /// 방 참가 시 자동으로 호출
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom);
        // 게임플레이 씬 호출, 로드씬
        // PhotonNetwork.LoadLevel("");
        if(PhotonNetwork.CountOfPlayersInRooms == 2)
        {
            LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);
        }
        else
        {
            Debug.Log("1명이 부족합니다.");
            LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);
        }
    }

    /// <summary>
    /// 랜덤 매칭(방 입장) 실패
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("JoinRandomFailed({0}): {1}", returnCode, message);
    }


}

