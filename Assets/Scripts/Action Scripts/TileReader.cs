using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using JAFprocedural;


[System.Serializable] public class FloatEvent : UnityEvent<float> { }


public class TileReader : MonoBehaviour
{

    public FloatEvent el0;

    public UnityEvent onScanEvent;

    private GameObject currentTile;

    void Start()
    {
        InitFloatEvent(el0);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ArenaTileProperties>() != null)
        {
            Debug.Log("TARGET: " + other.GetComponent<ArenaTileProperties>().properties.tileIDNum);

            if (scanMode == "Single")
            {
                if (other.GetComponent<ArenaTileProperties>().properties.is_walkable == true)
                {
                    
                    onScanEvent.Invoke();

                    el0.Invoke(other.GetComponent<ArenaTileProperties>().properties.elevation);
                    
                }
            }
            else if (scanMode == "Inverse")
            {
                if (!other.CompareTag(scanTarget))
                {
                    onScanEvent.Invoke();
                }
            }
        }
        

    }

    void InitFloatEvent(FloatEvent fl)
    {
        if (fl == null)
        {
            fl = new FloatEvent();
        }
    }
}

