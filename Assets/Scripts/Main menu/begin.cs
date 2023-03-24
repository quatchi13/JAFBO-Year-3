using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;  

public class begin : MonoBehaviour, IPointerDownHandler
{
    public GameObject ant;
    // Update is called once per frame
    void Update()
    {
        
    }
     
    public void OnPointerDown(PointerEventData evemtData){
        ant.GetComponent<Lobby>().SendCharacterSelection();
    }
}
