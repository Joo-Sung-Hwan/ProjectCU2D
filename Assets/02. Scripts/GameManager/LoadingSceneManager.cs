using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using Photon.Pun;
using Photon.Realtime;


public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Image imgLoadingBar;

    private static string nextSceneName;
    private static string groupToLoad;
    private static ESceneType sceneTypeToLoad;

    private float resourceProgress;


    [PunRPC]
    // static 정적함수 : 인스턴스화하지 않고도 아무데서나 호출가능한 로딩함수
    public static void LoadScene(string sceneName, string labelsToLoad, ESceneType sceneType)
    {
        nextSceneName = sceneName;
        groupToLoad = labelsToLoad;
        sceneTypeToLoad = sceneType;

        PhotonNetwork.LoadLevel("LoadingScene");
        //SceneManager.LoadScene("LoadingScene");
    }

    private async void Awake()
    {
        // 로딩씬에 진입하면 로딩 시작
        await LoadSceneAsync();

        /*
        if (sceneTypeToLoad == ESceneType.MainGame)
            GameManager.Instance.CreateMainGameScene();
        */
    }

    private async UniTask LoadSceneAsync()
    {
        imgLoadingBar.fillAmount = 0;

        // 리소스 로딩
        // AddressableManager의 LoadResources 함수를 UniTask로 호출
        /*
        await AddressableManager.Instance.LoadResourcesAsync(
            groupToLoad,
            (progress) =>
            {
                UpdateLoadingProgress(progress);
            }
        );
        */
        

        // 씬 로딩 비동기 메소드
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        asyncLoad.allowSceneActivation = false;

        // 로딩이 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            float sceneProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // 씬 로딩이 90% 이상이면 allowSceneActivation을 true로 변경하여 씬 변경하기
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            // 프레임 대기 (UniTask.DelayFrame()을 이용하여 한 프레임 대기)
            await UniTask.Yield();
        }
    }


    private void UpdateLoadingProgress(float progress)
        => imgLoadingBar.fillAmount = progress;
}
