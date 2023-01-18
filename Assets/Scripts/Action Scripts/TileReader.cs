using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileReader : MonoBehaviour
{
    public UnityEvent onScanEvent;

    [SerializeField]
    private string scanTarget;

    [SerializeField]
    private string scanMode;

    void OnTriggerEnter(Collider other)
    {
        if(scanMode == "Single")
        {
            if (other.CompareTag(scanTarget))
            {
               onScanEvent.Invoke();
            }
        }
        else if(scanMode == "Inverse")
        {
            if (!other.CompareTag(scanTarget))
            {
               onScanEvent.Invoke();
            }
        }
        
    }
}
