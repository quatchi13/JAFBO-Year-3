using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{



    // Update is called once per frame
    void Update()
    {
        //controls are subject to change/expand
        //Look inputs 
        if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector3(0,0,0);
                //change look dir
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                //change look dir
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                //change look dir
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                //change look dir
            }
            //confirm action
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //send confirm action message
            }
    }
}
