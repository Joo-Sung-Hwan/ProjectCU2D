using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using System.Threading;

using Object = UnityEngine.Object;
using System.CodeDom;
public class AddressableManager : Singleton<AddressableManager>
{    
    // 메모리에 올려둔 리소스가 저장된 딕셔너리
    private Dictionary<string, Dictionary<string, object>> resources = new Dictionary<string, Dictionary<string, object>>();
    // AsyncOperationHandle : 어드레서블의 로드할때 반환하는 인스턴스 (작업핸들)
    private Dictionary<string, AsyncOperationHandle> loadedGroups = new();

    public IReadOnlyDictionary<string, Dictionary<string, object>> Resources => resources;


    // 리소스를 어드레서블 그룹의 label 단위로 로드
    public async UniTask LoadResourcesAsync(string groupLabel, Action<float> progressCallback) 
    {
        using CancellationTokenSource disableCancellation = new CancellationTokenSource();

        // 1. 이미 로드한 그룹이면 return
        if (loadedGroups.TryGetValue(groupLabel, out var groups)) 
            return;

        try
        {
            // 2. LoadAsset's'Async : 해당 label의 모든 에셋을 한번에 로드 (그룹별로 label 통일 필수)
            // 정확히 말하면 그룹별 로드는 아니지만 그룹마다 label을 통일시켜서 로드하는 방법
            AsyncOperationHandle handle = Addressables.LoadAssetsAsync(groupLabel, (Object asset) =>
            {
                if (!resources.TryGetValue(groupLabel, out _))
                    resources[groupLabel] = new Dictionary<string, object>();

                resources[groupLabel][asset.name] = asset;
            });

            // 3. 로딩이 완료될 때까지 진행상태 업데이트
            while (!handle.IsDone)
            {
                progressCallback?.Invoke(handle.PercentComplete);
                await UniTask.Yield(cancellationToken: disableCancellation.Token);
            }

            // 4. 완료 상태 보고 (100%)
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
        // handle을 해제하면 그 핸들로 로드한 모든 리소스 해제
        if (loadedGroups.TryGetValue(groupLabel, out var handle))
        {
            Addressables.Release(handle);        
            loadedGroups.Remove(groupLabel); // 딕셔너리도 처리해주기

            if (resources.TryGetValue(groupLabel, out _))
            {
                // resources는 단순 에셋이 들어있는 딕셔너리일뿐
                // 리소스 메모리 해제는 위에서 핸들로 이미 처리함
                resources.Remove(groupLabel);
            }
        }
    }

    // 어드레서블 그룹에 저장한 리소스의 key값으로 가져오기
    // key : 어드레서블에 저장한 리소스의 이름
    public T GetResource<T>(string key) where T : Object
    {
        foreach (var resources in Resources.Values)
        {
            if (resources.TryGetValue(key, out var resource))
            {
                return resource as T;
            }
        }

        return null;
    }

    public void ReleaseAll()
    {
        foreach (var handle in loadedGroups.Values)
        {
            Addressables.Release(handle);
        }

        loadedGroups.Clear();
        resources.Clear();
    }
    private void OnDestroy()
    {
        ReleaseAll();
    }
}
