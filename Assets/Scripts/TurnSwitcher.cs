using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnSwitcher : MonoBehaviour
{
    public UnityEvent swapTurn;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            swapTurn.Invoke();
        }
    }
}
