using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    private string input;

    public Text chatBox;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadStringInput(string i){
        input = i;
        chatBox.text += ("\n" + input);
        Debug.Log(input);
    }
}
