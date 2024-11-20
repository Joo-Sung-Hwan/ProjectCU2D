using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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
        OnSettingNickName(PlayerPrefs.GetString("Nickname"));
        Debug.Log(PhotonNetwork.NickName); // �г��� �׽�Ʈ
    }

    public void Connect()
    {

        if (isConnecting) return;

        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            Debug.Log("�г��� ����");
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

    // ���⼭���� MonoBehaviourPunCallbacks�� override �޼����
    public override void OnConnected()
    {
        Debug.Log("���� ����");
    }

    public override void OnConnectedToMaster()
    {
        Debug.LogFormat("������ ���� ����: {0}", PhotonNetwork.NickName);
        isConnecting = false;
        PhotonNetwork.JoinRandomOrCreateRoom(
            expectedMaxPlayers: 2,
            roomOptions: new RoomOptions { MaxPlayers = 2 }
        );
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("���� ����: {0}", cause);
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"�� ���� ����! ���� �ο�: {PhotonNetwork.CurrentRoom.PlayerCount}");

#if UNITY_EDITOR
        LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);
#endif


        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log($"�ٸ� �÷��̾� ����! ���� �ο�: {PhotonNetwork.CurrentRoom.PlayerCount}");

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("���� ��Ī ����({0}): {1}", returnCode, message);
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
}