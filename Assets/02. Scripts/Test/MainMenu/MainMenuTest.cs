using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTest : MonoBehaviour
{
    public void BtnTest()
    {
        LoadingSceneManager.LoadScene("CombatTestScene", "TestB", ESceneType.MainGame);

        //AddressableManager.Instance.ReleaseGroup("TestA");
    }
}
