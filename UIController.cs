using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

   
}
