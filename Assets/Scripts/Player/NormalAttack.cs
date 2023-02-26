using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public GameObject remotePlayer;

    public GameObject attackButton;

    public int damage = 2;

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
        remotePlayer.GetComponent<HealthManager>().AlterHealth(-damage);
    }
}
