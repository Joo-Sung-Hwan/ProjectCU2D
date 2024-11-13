using Cinemachine;
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
    }
}
