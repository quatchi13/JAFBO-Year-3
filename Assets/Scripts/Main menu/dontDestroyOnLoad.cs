using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFnetwork;

public class dontDestroyOnLoad : MonoBehaviour
{
    //florence is 0
    //death is 1
    public int character;
    public Lobby lob;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(lob);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!didntdestroy) DontDestroyOnLoad(lob);
    }
}
