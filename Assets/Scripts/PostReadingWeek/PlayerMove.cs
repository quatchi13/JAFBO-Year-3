using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public void MoveUp()
    {
        gameObject.transform.position += new Vector3(0,0,1);
    }
    public void MoveDown()
    {
        gameObject.transform.position += new Vector3(0,0,-1);
    }
    public void MoveLeft()
    {
        gameObject.transform.position += new Vector3(1,0,0);
    }

    public void MoveRight()
    {
        gameObject.transform.position += new Vector3(-1,0,0);
    }
}
