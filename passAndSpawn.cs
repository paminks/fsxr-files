using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passAndSpawn : MonoBehaviour
{
    public GameObject roadPiecePrefab;
    public GameObject roadPiecePositionObject;
    private Vector2 currentPosition;

    private void Start()
    {
        currentPosition = roadPiecePositionObject.transform.position;
        Debug.Log("d");

    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            spawn();
        }
    }

    public void spawn()
    {
        int randomAngle = Random.Range(-25, 25);
        Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
        currentPosition += direction * 2; // multiply by 2 to increase distance between road pieces
        GameObject newRoad = Instantiate(roadPiecePrefab, currentPosition, Quaternion.Euler(0, 0, randomAngle));

        Destroy(newRoad, 12f);
    }

}
