using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTest : MonoBehaviour
{
    public void BtnTest()
    {
        List<string> levelResources = new List<string> { };
        LoadingSceneManager.LoadScene("CombatTestScene", levelResources, ESceneType.MainGame);
    }
}
