using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFnetwork;

public class NormalAttack : MonoBehaviour
{
    public GameObject remotePlayer;

    public GameObject attackButton;

    void Update()
    {
        if(Vector3.Distance(transform.position, remotePlayer.transform.position) == 1.0f && gameObject.GetComponent<ActionPointsManager>().actions > 1)
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

        BasicAttackChar attackChar = new BasicAttackChar();
        attackChar.Setup(NetworkParser.GetPCIndex(gameObject), NetworkParser.GetPCIndex(remotePlayer), new Vector3 (0,0,0));
        attackChar.Execute();
        NetworkParser.localGameplayCommands.Enqueue(attackChar);

    }
}
