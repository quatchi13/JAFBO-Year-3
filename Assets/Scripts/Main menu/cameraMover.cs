using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMover : MonoBehaviour
{
    bool started = true; 

    public List<Vector3> pointList;
    float speed = 0.1f; 
    int pointNum = 0;
    int rotateNum = 1;
    private Vector3 rotationPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

 
    if (started == true){
    transform.position = Vector3.MoveTowards(transform.position, pointList[(int)pointNum], speed);
    rotationPoint = Vector3.RotateTowards(transform.forward,pointList[(int)rotateNum] , 10, 10.0f);
    transform.LookAt (rotationPoint);
    Debug.Log(rotationPoint);
    Debug.Log(pointList[(int)rotateNum]);

     if(transform.position == pointList[(int)pointNum]){

        if(pointNum != 3){
        pointNum++;
        }
        else{
            pointNum = 0;
        }

        if(rotateNum != 3){
        rotateNum++;

        }
        else{
            rotateNum = 0;
        }

        Debug.Log(pointNum);
        Debug.Log(rotateNum);
     }
    }


    }

    public void select(){
        started = false;
        transform.position = pointList[(int)4];
        transform.LookAt(pointList[(int)5], Vector3.up);

    }
}
