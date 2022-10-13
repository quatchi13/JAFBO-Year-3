using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileReader : MonoBehaviour
{

    private GameObject currentTile;

    void Awake()
    {
        transform.parent.gameObject.GetComponent<PlayerInput>().AddScanner(gameObject);
        //create tile detection box
        gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f, 0.5f);
        gameObject.GetComponent<BoxCollider>().center = new Vector3(1.5f, 0f, 0f);
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        //turn into a switch statement please
        currentTile = other.gameObject;
        if (other.CompareTag("Ground"))
        {
            
            transform.parent.gameObject.GetComponent<PlayerInput>().SetCanMoveState(true);
            Debug.Log("Ground Tile Found");
        }
        else if (other.CompareTag("Rock"))
        {
            Debug.Log("Rock Tile Found");
        }
        else if (other.CompareTag("Water"))
        {
            Debug.Log("Water Tile Found");
        }
        else if (other.CompareTag("Tree"))
        {
            Debug.Log("Tree Tile Found");
        }
        else if (other.CompareTag("Boundary"))
        {
            Debug.Log("Boundary Tile Found");
            gameObject.GetComponent<PlayerInput>().SetCanMoveState(false);
        }

    }
}
