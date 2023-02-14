using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;  

public class begin : MonoBehaviour, IPointerDownHandler
{

    // Update is called once per frame
    void Update()
    {
        
    }
     public void OnPointerDown(PointerEventData evemtData){
        SceneManager.LoadScene("Game"); 
   }
}
