using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatToggle : MonoBehaviour
{
    public Vector3 onScreen;
    public Vector3 offScreen;
    private bool screenBool = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            if(screenBool == false){
                gameObject.transform.position = onScreen;
                screenBool = true;
            }
            else if(screenBool == true){
                gameObject.transform.position = offScreen;
                screenBool = false;
            }
        }
    }
}
