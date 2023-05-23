using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthSystem : MonoBehaviour
{   
    [SerializeField]
    private Slider slider;

    public PlayerStatus playerStatus; // refer to player (1)

    public void SetUp()
    {   
        playerStatus.healthChanged += HealthFunc_healthChanged; // set ability to event healtChanged
    }

    public void Start()
    {
        slider.maxValue = playerStatus.maxHealth;
        slider.value = playerStatus.maxHealth;
    }

    private void HealthFunc_healthChanged(object sender, System.EventArgs e)
    {  
        slider.value = playerStatus.health; // change health bar value each frame
    }


}
