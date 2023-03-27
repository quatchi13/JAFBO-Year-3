using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Follow : MonoBehaviour
{
    private GameObject player;
    private GameObject otherPlayer;
    private GameObject skyCam;
    public static bool isOther;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        otherPlayer = GameObject.FindWithTag("OtherPlayer");
        skyCam = GameObject.Find("Sky camera");

    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.H)){
        isOther = !isOther;
       }

       if(!isOther){
            gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0,15,-13);
            gameObject.GetComponent<CinemachineVirtualCamera>().LookAt = player.transform;
            gameObject.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
            skyCam.transform.rotation = Quaternion.Euler(90, 0, 0); 
       }
       else if(isOther){
            gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0,15,13);
            gameObject.GetComponent<CinemachineVirtualCamera>().LookAt = otherPlayer.transform;
            gameObject.GetComponent<CinemachineVirtualCamera>().Follow = otherPlayer.transform;
            skyCam.transform.rotation = Quaternion.Euler(90, 180, 0); 
       }
    }
}
