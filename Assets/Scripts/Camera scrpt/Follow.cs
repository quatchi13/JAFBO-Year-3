using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Follow : MonoBehaviour
{
    private GameObject player;
    private GameObject otherPlayer;
    private GameObject skyCam;
    public static bool isP2 = false;
    public static bool isShowingOther = false;


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
        Vector3 fOffset = new Vector3(0, 15, -13);
        Quaternion rotEul = Quaternion.Euler(90, 0, 0);
        if (isP2)
        {
            fOffset.z = 13;
            rotEul = Quaternion.Euler(90, 180, 0);
        }
       

       if(!isShowingOther){
            gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = fOffset;
            gameObject.GetComponent<CinemachineVirtualCamera>().LookAt = player.transform;
            gameObject.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
            skyCam.transform.rotation = rotEul; 
       }
       else{
            gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = fOffset;
            gameObject.GetComponent<CinemachineVirtualCamera>().LookAt = otherPlayer.transform;
            gameObject.GetComponent<CinemachineVirtualCamera>().Follow = otherPlayer.transform;
            skyCam.transform.rotation = rotEul; 
       }
    }


    
}
