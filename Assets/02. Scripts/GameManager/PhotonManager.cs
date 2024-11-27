using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "0.0.1";
    private bool isConnecting = false;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        //OnSettingNickName("123");
        OnSettingNickName(PlayerPrefs.GetString("Nickname"));
        Debug.Log(PhotonNetwork.NickName); // 닉네임 테스트
    }

    public void Connect()
    {

        if (isConnecting) return;

        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            Debug.Log("닉네임 설정");
            //return;
        }

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomOrCreateRoom(
                expectedMaxPlayers: 2,
                roomOptions: new RoomOptions { MaxPlayers = 2 }
            );
        }
        else
        {
            isConnecting = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void OnSettingNickName(string name)
    {
        PhotonNetwork.NickName = name;
    }

    // 여기서부터 MonoBehaviourPunCallbacks의 override 메서드들
    public override void OnConnected()
    {
        Debug.Log("연결 성공");
    }

    public override void OnConnectedToMaster()
    {
        Debug.LogFormat("마스터 서버 접속: {0}", PhotonNetwork.NickName);
        isConnecting = false;
        PhotonNetwork.JoinRandomOrCreateRoom(
            expectedMaxPlayers: 2,
            roomOptions: new RoomOptions { MaxPlayers = 2 }
        );
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("연결 끊김: {0}", cause);
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"방 참가 성공! 현재 인원: {PhotonNetwork.CurrentRoom.PlayerCount}");
#if UNITY_EDITOR
        //LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);
        //PhotonNetwork.LoadLevel("CombatTestScene");
#endif

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);
            //PhotonNetwork.LoadLevel("CombatTestScene");
        }
    }


    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log($"다른 플레이어 입장! 현재 인원: {PhotonNetwork.CurrentRoom.PlayerCount}");


        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);
            //PhotonNetwork.LoadLevel("CombatTestScene");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("랜덤 매칭 실패({0}): {1}", returnCode, message);
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
}