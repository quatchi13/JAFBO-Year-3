using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSelections : MonoBehaviour
{

    public static ActiveSelections instance;

    private void Start() {
        if(!instance)
        {
            instance = this;
        }
    }

    [SerializeField]   
     private List<GameObject> selection;

    // Update is called once per frame
    void Update()
    {
       if(selection.Count > 0)
        {
            HighlightSelection();
        }
    }

    public void AddSelectable(GameObject selectable) 
    {
        selection.Add(selectable);
    }

    public void ClearSelection()
    {
        selection.Clear();
    }
    
    public void HighlightSelection()
    {
        for(int i = 0; i < selection.Count; i++)
        {
            if(!selection[i].GetComponent<Highlight>())
            {
                if(selection[i] == CliickDetection.instance.currentSelection)
                {
                    //special highlight
                    Debug.Log("Added");
                    selection[i].AddComponent<Highlight>();
                }
                else
                {
                    //regular highlight
                
                }
            }
            
        }
    }
}
