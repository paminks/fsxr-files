using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarChooseUI : MonoBehaviour
{
    public CarController carController;
    public GameObject panel;
    void Start()
    {
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        Time.timeScale = 0f;
        
    }

    public void Mclaren()
    {
        Debug.LogError("what");
        Time.timeScale = 1f;
        carController.orderNum = 1;
        panel.SetActive(false);
    }

    public void FerrariF13()
    {
        Time.timeScale = 1f;
        carController.orderNum = 2;
        panel.SetActive(false);
    }

    public void RenaultR29()
    {
        Time.timeScale = 1f;
        carController.orderNum = 3;
        panel.SetActive(false);
    }

    public void MercedesW11()
    {
        Time.timeScale = 1f;
        carController.orderNum = 4;
        panel.SetActive(false);
    }

    public void AMR23()
    {
        Time.timeScale = 1f;
        carController.orderNum = 5;
        panel.SetActive(false);
    }
}