using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class characterSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Sprite on;
    public Sprite off;

    public GameObject begin;
    public Vector3 beginLocation;
    private bool beginTime = false;

public void OnPointerEnter(PointerEventData eventData)
   {
    if(beginTime == false){
         gameObject.GetComponent<Image>().sprite = on;
    }
    
   }

     
public void OnPointerExit(PointerEventData eventData)
   {
      gameObject.GetComponent<Image>().sprite = off;
   }


    public void OnPointerDown(PointerEventData evemtData){
        beginTime = true;
        begin.transform.position = beginLocation;

   }

}
