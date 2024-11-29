using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;


public class ObjectPoolManager : MonoBehaviourPun
{
    public static ObjectPoolManager Instance;

    // 인스펙터에서 등록할 Pool
    [SerializeField] private List<Pool> datas;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private Transform objPoolTransform;

    [System.Serializable]
    public struct Pool
    {
        public string name;
        public int initialSize;
        public GameObject prefab;
    }

    private void Awake()
    {
        Instance = this;
        objPoolTransform = GetComponent<Transform>();
    }


    private void Start()  //오브젝트 미리 생성하는 과정.
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        if (!PhotonNetwork.IsMasterClient) return;
        foreach (Pool data in datas)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < data.initialSize; i++)
            {
                GameObject obj = PhotonNetwork.Instantiate(data.name, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(objPoolTransform, false);
                obj.GetComponent<PhotonView>().RPC("SetActiveRPC", RpcTarget.All,Vector3.zero, Quaternion.identity, false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(data.name, objectPool);
        }
    }

    public GameObject Get(string name, Vector3 position, Quaternion rotation) //오브젝트를 꺼내쓰는것.
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject obj = poolDictionary[name].Dequeue();
            obj.GetComponent<PhotonView>().RPC("SetActiveRPC", RpcTarget.All, position, rotation, true);
            poolDictionary[name].Enqueue(obj);
            return obj;
        }
        else
        {
            return null;
        }
    }
    public GameObject Get(string name, Transform tranform)
        => Get(name, tranform.position, tranform.rotation);

    public void Release(GameObject obj) //오브젝트 비활성화 하는 함수.
    {
        obj.GetComponent<PhotonView>().RPC("SetActiveRPC", RpcTarget.All, obj.transform.position, obj.transform.rotation, false);
    }
}