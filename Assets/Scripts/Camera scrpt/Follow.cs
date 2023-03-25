using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Follow : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().LookAt = GameObject.FindWithTag("Player").transform;
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
