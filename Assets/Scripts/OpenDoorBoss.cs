using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoorBoss : MonoBehaviour
{
    [SerializeField]
    private int sceneNum;
    void OnCollisionEnter2D(Collision2D collider)
    {   
        if (collider.gameObject.CompareTag("Player"))
        {
            if (collider.gameObject.GetComponent<PlayerStatus>().keys == 3)
            {
                SceneManager.LoadScene(sceneNum);
                collider.gameObject.transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}
