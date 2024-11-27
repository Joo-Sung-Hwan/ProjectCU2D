using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoolObj : MonoBehaviour
{

    [PunRPC]
    public void SetActiveRPC(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
}
