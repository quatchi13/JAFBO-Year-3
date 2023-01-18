using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOptions : MonoBehaviour
{
    public string direction;
    //this script goes on each child object with one box collider for each of the 4 directions
[SerializeField]
    private ActionCoroutines action;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Ground")
        {
            action.SetValidDirection(direction);
        }
        
    }
}
