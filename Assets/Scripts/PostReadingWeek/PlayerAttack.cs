using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject remotePlayer;

    private void Start() 
    {
        
    }
    public void Attack()
    {
        remotePlayer.GetComponent<PlayerHealthManager>().AlterHealth(-2);
    }
}
