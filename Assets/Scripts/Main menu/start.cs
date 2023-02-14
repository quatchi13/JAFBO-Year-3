using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class start : MonoBehaviour, IPointerDownHandler
{
    public GameObject options;
    public GameObject exit;
    public GameObject title;
    public GameObject splitscreen;
    public GameObject online;
    public Vector3 splitLocation;
    public Vector3 onlineLocation;
    public Vector3 offScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnPointerDown(PointerEventData evemtData){
        transform.position = offScreen;
        options.transform.position = offScreen;
        exit.transform.position = offScreen;
        title.transform.position = offScreen;
        splitscreen.transform.position = splitLocation;
        online.transform.position = onlineLocation;
   }
}
