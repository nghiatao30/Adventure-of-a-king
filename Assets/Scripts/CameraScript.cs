using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    private float left, right, bottom;
    private string sceneName;
    private static CameraScript Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetUp();
    }

    void Update()
    {   
        if(sceneName != SceneManager.GetActiveScene().name)
        {
            SetUp();
        }
        if(transform.position.x <= left)
        {
            transform.position = new Vector3(left, transform.position.y, transform.position.z);
        }
        else if(transform.position.x >= right)
        {
            transform.position = new Vector3(right, transform.position.y, transform.position.z);
        }
        
        if(transform.position.y <= bottom)
        {
            transform.position = new Vector3(transform.position.x, bottom, transform.position.z);
        }
    }

    void SetUp()
    {
        left = GameObject.Find("Left").transform.position.x;
        right = GameObject.Find("Right").transform.position.x;
        bottom = GameObject.Find("Bottom").transform.position.y;
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
    }
}
