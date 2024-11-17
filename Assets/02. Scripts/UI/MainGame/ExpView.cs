using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpView : MonoBehaviour
{
    private Slider expBar;


    private void Awake()
    {
        expBar = GetComponent<Slider>();
    }

    public void SetExpBar(float ratio) 
        => expBar.value = ratio;
}
