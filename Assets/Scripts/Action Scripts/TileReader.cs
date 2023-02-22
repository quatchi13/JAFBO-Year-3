using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileReader : MonoBehaviour
{

    private GameObject currentTile;

    void OnTriggerEnter(Collider other)
    {
        currentTile = other.gameObject;
        if (other.CompareTag("Ground"))
        {
            ActiveSelections.instance.AddSelectable(currentTile);
            transform.parent.gameObject.GetComponent<PlayerInput>().SetCanMoveState(true);
        }
    }
}
