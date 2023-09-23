using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void Start()
    {
        Screen.SetResolution(800, 700, FullScreenMode.Windowed);
    }

    public void PlayMonza()
    {
        SceneManager.LoadScene("Monza");
    }
    public void PlayInfiniteMode()
    {
        //SceneManager.LoadScene("Level1");
    }

    public void exitGame()
    {
        Application.Quit();
    }
    
    

}
