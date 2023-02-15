using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField] private float yOffset;
    void Update()
    {
        gameObject.transform.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z);
    }
}
