using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFnetwork;

public class NormalAttack : MonoBehaviour
{
    public GameObject remotePlayer;

    public GameObject attackButton;
    private Animator animator;
    
    void Awake() 
    {
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, remotePlayer.transform.position) == 1.0f && ActionPointsManager.instance.actions > 1)
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
        //attack time but reference antenna to know what characacter 
        animator.Play("Basic Attack");
        //or someelse that listens to antenna can tell it
        BasicAttackChar attackChar = new BasicAttackChar();
        attackChar.Setup(NetworkParser.GetPCIndex(gameObject), NetworkParser.GetPCIndex(remotePlayer), new Vector3 (0,0,0));
        attackChar.Execute();
        NetworkParser.localGameplayCommands.Enqueue(attackChar);

    }
}
