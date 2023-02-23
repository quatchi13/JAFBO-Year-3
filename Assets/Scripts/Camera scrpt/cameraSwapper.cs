using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class cameraSwapper : MonoBehaviour
{
   [SerializeField] private InputAction action;
    public CinemachineVirtualCamera follow;
    public CinemachineVirtualCamera sky;
    private bool followCam = false;


    private void OnEnable() {
        action.Enable();
    }

    private void OnDisable() {
        action.Disable();
    }


    void Start()
    {
        action.performed += _ => SwapCam();
    }

    private void SwapCam(){
        if(followCam){
            sky.Priority = 0;
            follow.Priority = 1;
        }
        else{
            follow.Priority = 0;
            sky.Priority = 1;
        }
        followCam = !followCam; 
        Debug.Log(followCam);
    }

}
