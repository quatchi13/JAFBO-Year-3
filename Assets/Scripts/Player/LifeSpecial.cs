using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JAFnetwork;

public class LifeSpecial : MonoBehaviour
{
    public GameObject specialButton;
    public bool isActive = false;

    public int cooldown;
    private int upTime = 2;

    void Awake()
    {
        //Set each of the buttons to a reference of the Ui button that already exists. We need to do this because it is a prefab being instantiated and cant store references. Therefore Awake

        specialButton = GameObject.Find("SpecialAttackButton");
    }

    private void Start()
    {
        specialButton.GetComponent<Button>().onClick.AddListener(Special);
    }

    private void Update() 
    {
        if(gameObject.GetComponent<ActionPointsManager>().actions > 4 && cooldown < 1)
        {
            specialButton.SetActive(true);
        }    
        else
        {
            specialButton.SetActive(false);
        }
    }

    public void Special()
    {
        cooldown = upTime;
        ChangeStatChar statCom = new ChangeStatChar();
        statCom.Setup(GameObject_Manager.localPlayerIndex, 0, 32);
        statCom.Execute();
        NetworkParser.localGameplayCommands.Enqueue(statCom);
        isActive = true;
    }
}
