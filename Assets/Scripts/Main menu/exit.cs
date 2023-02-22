using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class exit : MonoBehaviour, IPointerDownHandler

{
     public void OnPointerDown(PointerEventData evemtData){
        Debug.Log("exitgame");  
        Application.Quit();  
   }
}
