using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public GameObject devMenu;
    public CarController cc;
    public bool dm;
    public CinemachineVirtualCamera cmVCam;
    public Transform AiCar1;
    public Transform playerCar;
    public AIController aic;
    public TextMeshProUGUI aiCheckpointText;
    public TextMeshProUGUI playerCheckpointText;
    public void Start()
    {
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        aic = GameObject.FindGameObjectWithTag("AI1").GetComponent<AIController>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("fsxrfiles-main/Scenes/MainMenu");
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            devMenu.SetActive(true);
            dm = true;
        }

        if (Input.GetKeyDown((KeyCode.Q)))
        {
            devMenu.SetActive(false);
            dm = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && dm ==true)
        {
            cmVCam.Follow = AiCar1;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && dm ==true)
        {
            cmVCam.Follow = playerCar;
        }
        
        playerCheckpointText.text = "Player passed " + cc.playerCheckpoints.ToString() + "points";
        aiCheckpointText.text = "AI 1 passed " + aic.AIcheckpoints.ToString() + "points";
        
    }

    public void restartGame()
    {
        SceneManager.LoadScene("fsxrfiles-main/Scenes/Monza");
    }
    public void backtoMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.LogError("nigger what");
    }
}
