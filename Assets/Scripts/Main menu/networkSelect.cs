using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class networkSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Sprite on;
    public Sprite off;
    public Camera camera;

    public GameObject florence;
    public GameObject death;
    public GameObject otherNetwork;
    public GameObject ant;
    public Vector3 florenceLocation;
    public Vector3 deathLocation;
    public Vector3 offScreen;


public void OnPointerEnter(PointerEventData eventData)
   {
         gameObject.GetComponent<Image>().sprite = on;
   }

     
public void OnPointerExit(PointerEventData eventData)
   {
      gameObject.GetComponent<Image>().sprite = off;
   }
   
     public void OnPointerDown(PointerEventData evemtData){

        camera.GetComponent<cameraMover>().select();
        transform.position = offScreen;
        otherNetwork.transform.position = offScreen;

        florence.transform.position = florenceLocation;
        death.transform.position = deathLocation;

        ant.GetComponent<Lobby>().StartClient();
   }
}
