using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private LevelUpController levelUpController;
    private ExpController expController;

    public LevelUpController LevelUpController => levelUpController;


    private void Awake()
    {
        expController = GetComponent<ExpController>();
    }

    public void InitializeUIController()
    {
        expController.InitializeExpController();
        levelUpController.gameObject.SetActive(false);
    }
}
