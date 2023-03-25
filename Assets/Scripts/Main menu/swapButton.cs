using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class swapButton : MonoBehaviour, IPointerDownHandler
{
    public int character = 0;
    public Vector3 cardLocation;
    public Vector3 spinLocation;
    public Vector3 offScreenCard;
    public Vector3 offScreenModel;
    public GameObject florenceCard;
    public GameObject florenceModel;
    public GameObject deathCard;
    public GameObject deathModel;
    public GameObject lifeCard;
    public GameObject lifeModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData evemtData){
     
        if(character <= 1) 
        {character++;}

        else if(character == 2) 
        {character = 0;}

        SwapTime();
    }
    public void SwapTime(){
        if(character == 0) {
            lifeCard.transform.position = offScreenCard;
            lifeModel.transform.position = offScreenModel;
            florenceCard.transform.position = cardLocation;
            florenceModel.transform.position = spinLocation;
        }

        if(character == 1){
            florenceCard.transform.position = offScreenCard;
            florenceModel.transform.position = offScreenModel;
            deathCard.transform.position = cardLocation;
            deathModel.transform.position = spinLocation;
        }

        if(character == 2){
            deathCard.transform.position = offScreenCard;
            deathModel.transform.position = offScreenModel;
            lifeCard.transform.position = cardLocation;
            lifeModel.transform.position = spinLocation;
        }
}
}


