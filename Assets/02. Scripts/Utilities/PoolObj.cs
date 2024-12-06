using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoolObj : MonoBehaviour
{

    [PunRPC]
    public void SetActiveRPC(Vector3 position, Quaternion rotation, bool isActive)
    {
        transform.position = position;
        transform.rotation = rotation;
        this.gameObject.SetActive(isActive);
    }

    [PunRPC]
    public void SetParentRPC(Transform trans)
    {
        transform.SetParent(trans, false);
    }
}
