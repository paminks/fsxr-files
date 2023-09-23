using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandlePlayerInput : MonoBehaviour
{
    CarController carcontroller;
    
    public float gas;
    public float brake;
    public float maxGas = 1f;
    public float maxBrake = 1f;
    public float minGas = 0;
    public float minBrake = 0;  
    private void Awake()
    {
        carcontroller = GetComponent<CarController>();
    }
    public void OnGasDown()
    {
        
        gas+=0.01f;

       
    }
    public void OnGasUp()
    {
        gas -= 0.01f;

        
    }
    public void OnBrakeDown()
    {
        
        brake += 0.1f;
       

    }
    public void OnBrakeUp() 
    {   
        brake -= 0.1f;
        
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        inputVector.x = SimpleInput.GetAxis("Horizontal");
        inputVector.y = gas;
        
        Vector2 inputVector2 = Vector2.zero;
        inputVector2.y = brake;
        
        Vector2 inputVectorPC = Vector2.zero;
        inputVectorPC.x = Input.GetAxisRaw("Horizontal");
        inputVectorPC.y = Input.GetAxisRaw("Vertical");
        
        Vector2 inputVectorPC2 = Vector2.zero;
        inputVectorPC.y = Input.GetAxisRaw("Vertical");


        
        //carcontroller.SetInputVector2(inputVector2);
        //carcontroller.SetInputVector(inputVector);
        

       carcontroller.SetInputVector(inputVectorPC);
       carcontroller.SetInputVector2(inputVectorPC2);


    }
}
