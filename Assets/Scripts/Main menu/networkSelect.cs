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

    public GameObject florenceCard;
    public GameObject florenceModel;
    public GameObject swapButton;

    public GameObject ant;

    public GameObject otherNetwork;
    public Vector3 florenceCardLocation;
    public Vector3 florenceModelLocation;
    public Vector3 swapLocation;
    public Vector3 offScreen;



    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = on;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = off;
    }

    public void OnPointerDown(PointerEventData evemtData)
    {

        camera.GetComponent<cameraMover>().select();
        transform.position = offScreen;
        otherNetwork.transform.position = offScreen;
        florenceCard.transform.position = florenceCardLocation;
        florenceModel.transform.position = florenceModelLocation;
        swapButton.transform.position = swapLocation;


        ant.GetComponent<Lobby>().StartClient();
    }
}