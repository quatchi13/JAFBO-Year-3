using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVIL_Player : MonoBehaviour
{
    public GameObject[] chars = new GameObject[3];

    void Awake()
    {
        int ind = (GameObject_Manager.localPlayerIndex == 0) ? 1 : 0;
        Vector3 startPos = (ind == 1) ? new Vector3(28f, 0.75f, -4f) : new Vector3(1f, 0.75f, -25f);
        Instantiate(chars[GameObject_Manager.selectedCharacters[1]], startPos, Quaternion.identity);
    }
}
