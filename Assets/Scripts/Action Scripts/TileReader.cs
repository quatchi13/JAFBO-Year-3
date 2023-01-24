using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileReader : MonoBehaviour
{
    [SerializeField]
    private PlayerInput player;

    public UnityEvent onScanEvent;

    [SerializeField]
    private string scanTarget;

    [SerializeField]
    private string scanMode;

    private GameObject detectedObject;    

    void OnTriggerEnter(Collider other)
    {
        detectedObject = other.gameObject;
        if(scanMode == "Single")
        {
            if (other.CompareTag(scanTarget))
            {
               
                player.UpdateTarget(detectedObject);
               onScanEvent.Invoke();
            }
        }
        
        else if(scanMode == "Inverse")
        {
            if (!other.CompareTag(scanTarget))
            {
                
                player.UpdateTarget(detectedObject);
               onScanEvent.Invoke();
            }
        }
        
    }

    
}
