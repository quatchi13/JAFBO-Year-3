using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieReader : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider currentCollider;

    void Awake()
    {
       
        //create tile detection box
        currentCollider = gameObject.AddComponent<BoxCollider>();
        currentCollider.size = new Vector3(0.5f, 0.5f, 0.5f);
        currentCollider.center = new Vector3(1.5f, 0f, 0f);
        currentCollider.isTrigger = true;

        currentCollider = gameObject.AddComponent<BoxCollider>();
        currentCollider.size = new Vector3(0.5f, 0.5f, 0.5f);
        currentCollider.center = new Vector3(3.0f, 0f, 0f);
        currentCollider.isTrigger = true;

        
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            
              Debug.Log("Enemy Spotted");
              ActiveSelections.instance.AddSelectable(other.gameObject);
        }
        

    }
}
