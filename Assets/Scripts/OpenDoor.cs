using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private int sceneNum;

    public bool isBossDied = false;

    void OnTriggerEnter2D(Collider2D collider)
    {   
        if(isBossDied)
        {
            SceneManager.LoadScene(sceneNum);
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}
