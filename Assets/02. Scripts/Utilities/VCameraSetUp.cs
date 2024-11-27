using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCameraSetUp : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;



    private void OnEnable()
    {
        GameManager.Instance.OnMainGameStarted += Instance_OnMainGameStarted;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnMainGameStarted -= Instance_OnMainGameStarted;
    }

    private void Instance_OnMainGameStarted()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();

        vcam.Follow = GameManager.Instance.Player.transform;


        Debug.Log($"VCAM!!!! - {vcam.Follow} , player is null?? : {GameManager.Instance.Player == null}");
        if (GameManager.Instance.Player != null)
            Debug.Log($"player is mine???? : {GameManager.Instance.Player.GetComponent<PhotonView>().IsMine} ");
    }
}
