using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using System.Threading;

using Object = UnityEngine.Object;
public class AddressableManager : Singleton<AddressableManager>
{
    // �޸𸮿� �÷��� ���ҽ��� ����� ��ųʸ�
    private Dictionary<string, object> resources = new Dictionary<string, object>();
    // AsyncOperationHandle : ��巹������ �ε��Ҷ� ��ȯ�ϴ� �ν��Ͻ� (�۾��ڵ�)
    private Dictionary<string, AsyncOperationHandle> loadedGroups = new();

    public IReadOnlyDictionary<string, object> Resources => resources;


    // ���ҽ��� ��巹���� �׷��� label ������ �ε�
    public async UniTask LoadResources(string groupLabel, Action<float> progressCallback) 
    {
        // 1. �̹� �ε��� �׷��̸� return
        if (loadedGroups.TryGetValue(groupLabel, out var groups)) 
            return;

        try
        {
            // 2. LoadAsset's'Async : �ش� label�� ��� ������ �ѹ��� �ε� (�׷캰�� label ���� �ʼ�)
            // ��Ȯ�� ���ϸ� �׷캰 �ε�� �ƴ����� �׷츶�� label�� ���Ͻ��Ѽ� �ε��ϴ� ���
            AsyncOperationHandle handle = Addressables.LoadAssetsAsync(groupLabel, (Object asset) =>
            {
                resources.Add(asset.name, asset);
            });

            // �ε��� �Ϸ�� ������ ������� ������Ʈ
            while (!handle.IsDone)
            {
                progressCallback?.Invoke(handle.PercentComplete);
                await UniTask.Yield();
            }

            // �Ϸ� ���� ���� (100%)
            progressCallback?.Invoke(1.0f);

            loadedGroups.Add(groupLabel, handle);
        }
        catch (Exception e)
        {
            Debug.LogError($"LoadResources Failed!!!! - {groupLabel}: {e.Message}");
        }
    }

    public void ReleaseGroup(string groupLabel)
    {
        // handle�� �����ϸ� �� �ڵ�� �ε��� ��� ���ҽ� ����
        if (loadedGroups.TryGetValue(groupLabel, out var handle))
            Addressables.Release(handle);        
    }

    // ��巹���� �׷쿡 ������ ���ҽ��� key������ ��������
    // key : ��巹���� ������ ���ҽ��� �̸�
    public T GetResource<T>(string key) where T : Object
    {
        if (Resources.TryGetValue(key, out var resource))
        {
            return resource as T;
        }
        return null;
    }

    public void ReleaseAll()
    {
        if (resources.Count == 0) return;

        foreach (var resource in resources)
        {
            Addressables.Release(resource.Value);
        }
    }
    private void OnDestroy()
    {
        ReleaseAll();
    }
}
