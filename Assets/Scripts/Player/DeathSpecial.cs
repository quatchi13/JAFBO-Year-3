using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JAFnetwork;

public class DeathSpecial : MonoBehaviour
{
    public GameObject specialButton;

    public GameObject remotePlayer;

    public bool isActive = false;

    public int cooldown;
    private int upTime = 2;

    private Animator anim;

     public GameObject sound;

    void Start()
    {
        //Set each of the buttons to a reference of the Ui button that already exists. We need to do this because it is a prefab being instantiated and cant store references. Therefore Awake
        int remInd = (GameObject_Manager.localPlayerIndex == 0) ? 1 : 0;
        remotePlayer = NetworkParser.playerCharacters[remInd];

        specialButton = GameObject.Find("SpecialAttackButton");
        specialButton.GetComponent<Button>().onClick.AddListener(Special);

        anim = GetComponent<Animator>();
        sound = GameObject.Find("SoundManager");
    }

    private void Update() 
    {
        if(gameObject.GetComponent<ActionPointsManager>().actions > 4 && cooldown <1)
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
        ChangeFlagChar flagCom = new ChangeFlagChar();
        flagCom.Setup(NetworkParser.GetPCIndex(remotePlayer), 1, true);
        flagCom.Execute();
        NetworkParser.localGameplayCommands.Enqueue(flagCom);
        isActive = true;
         sound.GetComponent<SoundManager>().DeathSound();  
    }
}
