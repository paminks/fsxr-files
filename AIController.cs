using System;
using System.Collections;
using UnityEngine;
using PathCreation;

using Unity.VisualScripting;

public class AIController : MonoBehaviour
{
    
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;
    public CarController cc;
    public float AIcheckpoints;
    void Start()
    {
        
        cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        if (pathCreator != null && cc.orderNum !=0)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = Quaternion.FromToRotation(Vector3.right, pathCreator.path.GetNormalAtDistance(distanceTravelled));
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("StartBrakeZone"))
        {
            speed = -45f;  
        }

        if (other.CompareTag("ExitBrakeZone"))
        {
            speed = -53;
        }

        if (other.CompareTag("Checkpoint"))
        {
            AIcheckpoints+= 1f;
            
        }  

        
    }

    public IEnumerator slowCar()
    {
        speed = -7;
        yield return new WaitForSeconds(1f);
        speed = -13;
    }
}

