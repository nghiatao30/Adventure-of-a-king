using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameHandler : MonoBehaviour
{   
    // Update is called once per frame
    [SerializeField]
    private PlayerStatus playerStatus;

    public Button restartBut;
    public Button continueBut;

    public GameObject menuScence;

    public static bool isGameActive = false;
    public static bool isGamePause = false;



    void Start()
    {
        playerStatus.healthBar.SetUp(); // start to build healt bar system
    }

    public void OpenMenu()
    {
        menuScence.SetActive(true);
        isGamePause = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        menuScence.SetActive(false);
        isGamePause = false;
        Time.timeScale = 1f;
    }

    public void TryAgain()
    {   
        playerStatus.LoadPlayerData();
        menuScence.SetActive(false);
        isGamePause = false;
        Time.timeScale = 1f;
    }

}
