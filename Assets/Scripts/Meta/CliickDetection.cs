using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliickDetection : MonoBehaviour
{

    public static CliickDetection instance;

[SerializeField]
    private Camera camera;

    public GameObject currentSelection;

private void Start() {
    if(!instance)
    {
        instance = this;
    }

}
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DetectObjects();
        }
        
    }

    public void DetectObjects()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
           currentSelection = hit.collider.gameObject;
        }
    }

    
}
