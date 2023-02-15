using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using JAFprocedural;


[System.Serializable] public class FloatEvent : UnityEvent<float> { }
[System.Serializable] public class GO_Event : UnityEvent<GameObject> { }


public class TileReader : MonoBehaviour
{

    public GameObject localPlayer;

    public FloatEvent el0;
    public GO_Event sendTarget;

    public UnityEvent onScanEvent;

    [SerializeField]
    private string scanTarget;

    [SerializeField]
    private string scanMode;

    void Start()
    {
        InitFloatEvent(el0);
        InitGOEvent(sendTarget);
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
                    if (localPlayer.GetComponent<PlayerInput>().IsAppropriateElevation(other.GetComponent<ArenaTileProperties>().properties.elevation))
                    {
                        onScanEvent.Invoke();

                        el0.Invoke(other.GetComponent<ArenaTileProperties>().properties.elevation);
                    }
                    
                    
                }
            }
        }
        if (scanMode == "Atk")
        {
            Debug.Log("Detected: " + other.tag);
            if (other.CompareTag(scanTarget))
            {
                sendTarget.Invoke(other.gameObject);
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

    void InitGOEvent(GO_Event g)
    {
        if(g == null)
        {
            g = new GO_Event();
        }
    }
}

