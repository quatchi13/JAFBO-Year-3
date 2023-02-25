using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour
{
    public GameObject remotePlayer;

    public void SpecialAttack()
    {
        remotePlayer.GetComponent<PlayerHealthManager>().AlterHealth(-5);
    }
}
