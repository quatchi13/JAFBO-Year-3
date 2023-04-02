using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JAFnetwork;

public class ActionPointsManager : MonoBehaviour
{

    public GameObject actionsText;
      public GameObject healthText;
    public int actions = 0;
    private void Start() 
    {
        actionsText = GameObject.Find("Actions Text");
        healthText = GameObject.Find("Health Text");
    }


    public void AlterActionNumber(int difference)
    {
        actions += difference;
        actions = (actions < 0) ? 0 : actions;

    }

    private void Update() 
    {
        actionsText.GetComponent<TMP_Text>().text = "Action Points: " + actions;
        healthText.GetComponent<TMP_Text>().text = "Health: " + gameObject.GetComponent<StatHolder>().stats[0];    
    }

    public void StartTurn()
    {
        actions = gameObject.GetComponent<StatHolder>().stats[2];
        GetComponent<PlayerInput>().isMyTurn = true;
        Debug.Log("okay, i can move now!");
    }

    
}
