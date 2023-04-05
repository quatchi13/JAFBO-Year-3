using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JAFnetwork;

public class NormalAttack : MonoBehaviour
{
    public GameObject remotePlayer;

    public GameObject attackButton;

    private Animator anim;

    void Start()
    {
        //Set each of the buttons to a reference of the Ui button that already exists. We need to do this because it is a prefab being instantiated and cant store references. Therefore Awake
        //remotePlayer = NetworkParser.playerCharacters[1];
        remotePlayer = GameObject.FindWithTag("OtherPlayer");

        attackButton = GameObject.Find("AttackButton");

        anim = GetComponent<Animator>();

        attackButton.GetComponent<Button>().onClick.AddListener(Attack);
        
        attackButton.SetActive(false);
    }
    

    void Update()
    {
        if(Vector3.Distance(transform.position, remotePlayer.transform.position) < 2.0f && gameObject.GetComponent<ActionPointsManager>().actions > 1)
        {
            attackButton.SetActive(true);
        }
        else
        {
            attackButton.SetActive(false);
        }
    }

    public void Attack()
    {
        Debug.Log("Smek");
        anim.Play("Layer 1.Basic Attack", 0, 0);
        anim.Play("Layer 2.Basic Attack 1", 0, 0);
        anim.Play("Layer 3.Basic Attack 2", 0, 0);
        anim.Play("Layer 4.Basic Attack 3", 0, 0);
        remotePlayer.GetComponent<StatHolder>().InflictDamage(gameObject.GetComponent<StatHolder>().stats[2]);
        BasicAttackChar attackChar = new BasicAttackChar();
        attackChar.Setup(NetworkParser.GetPCIndex(gameObject), NetworkParser.GetPCIndex(remotePlayer), new Vector3 (0,0,0));
        attackChar.Execute();
        NetworkParser.localGameplayCommands.Enqueue(attackChar);

        if(remotePlayer.GetComponent<StatHolder>().stats[0] <= 0)
        {
            MarkPlayerAsEliminated ripRemote = new MarkPlayerAsEliminated();
            ripRemote.Setup(NetworkParser.GetPCIndex(remotePlayer));
            NetworkParser.SendServerCommand(ripRemote);
        }

    }
}
