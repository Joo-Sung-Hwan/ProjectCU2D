using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private LevelUpController levelUpController;
    private ExpController expController;
    private StageInfoController stageInfoController;

    public LevelUpController LevelUpController => levelUpController;


    private void Awake()
    {
        expController = GetComponent<ExpController>();
        stageInfoController = GetComponent<StageInfoController>();
    }

    public void InitializeUIController()
    {
        expController.InitializeExpController();
        levelUpController.gameObject.SetActive(false);
        stageInfoController.InitializeStageInfoController();
    }
}
