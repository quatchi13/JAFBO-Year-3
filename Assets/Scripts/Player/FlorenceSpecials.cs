using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JAFnetwork;

public class FlorenceSpecials : MonoBehaviour
{
    public GameObject specialButton;

    public bool isActive = false;
    
    public int cooldown;
    private int upTime = 2;

    private Animator anim;

     public GameObject sound;

    void Awake()
    {
        //Set each of the buttons to a reference of the Ui button that already exists. We need to do this because it is a prefab being instantiated and cant store references. Therefore Awake

        specialButton = GameObject.Find("SpecialAttackButton");
    }

    private void Start()
    {
        specialButton.GetComponent<Button>().onClick.AddListener(Special);
        anim = GetComponent<Animator>();
        sound = GameObject.Find("SoundManager");
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
        anim.Play("Layer 1.Special 1", 0, 0);
        anim.Play("Layer 2.Special 1 1", 0, 0);
        anim.Play("Layer 3.Special 1 2", 0, 0);
        anim.Play("Layer 4.Special 1 3", 0, 0);
        cooldown = upTime;
        Debug.Log("cloaked");
        ChangeFlagChar flagCommand = new ChangeFlagChar();
        flagCommand.Setup(GameObject_Manager.localPlayerIndex, 0, true);
        flagCommand.Execute();
        NetworkParser.localGameplayCommands.Enqueue(flagCommand);
         sound.GetComponent<SoundManager>().FlorSound();  
    }
}
