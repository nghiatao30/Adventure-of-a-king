using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialete : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private float aDilate, bDilate;
    private float dialate;
    private float timeChange;

    void Start()
    {
        aDilate = -0.02f;
        bDilate = -0.63f;
        dialate = aDilate;
        timeChange = 0f;
    }

    void Update()
    {   
        if(Time.time > timeChange)
        {
            this.tmp.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dialate);
            if (dialate == aDilate) dialate = bDilate;
            else dialate = aDilate;

            timeChange = Time.time + 0.5f;
        }
        
    }
}
