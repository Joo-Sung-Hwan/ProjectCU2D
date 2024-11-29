using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;


public class ObjectPoolManager : MonoBehaviourPun
{
    public static ObjectPoolManager Instance;

    // �ν����Ϳ��� ����� Pool
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


    private void Start()  //������Ʈ �̸� �����ϴ� ����.
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool data in datas)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < data.initialSize; i++)
            {
                GameObject obj = PhotonNetwork.Instantiate(data.name, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(objPoolTransform, false);
                obj.GetComponent<PhotonView>().RPC("SetActiveRPC", RpcTarget.All, false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(data.name, objectPool);
        }
    }

    public GameObject Get(string name, Vector3 position, Quaternion rotation) //������Ʈ�� �������°�.
    {
        if (!poolDictionary.ContainsKey(name))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't excist.");
            return null;
        }

        GameObject obj = poolDictionary[name].Dequeue();
        obj.GetComponent<PhotonView>().RPC("SetActiveRPC", RpcTarget.All, true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        poolDictionary[name].Enqueue(obj);

        return obj;
    }
    public GameObject Get(string name, Transform tranform)
        => Get(name, tranform.position, tranform.rotation);

    public void Release(GameObject obj) //������Ʈ ��Ȱ��ȭ �ϴ� �Լ�.
    {
        obj.GetComponent<PhotonView>().RPC("SetActiveRPC", RpcTarget.All, false);
    }
}