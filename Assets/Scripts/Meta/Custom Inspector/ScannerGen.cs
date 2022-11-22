using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScannerGen : MonoBehaviour
{
    private void Start() {
        
    }
    public GameObject character;

[SerializeField]
    private GameObject scannerTemplate;
    
public string scannerName;



    public ArrayLayout data;
    




    private void Update() {
        
    }

    public void Generate()
    {
        GameObject newActionScanner = Instantiate(scannerTemplate, character.transform);
        newActionScanner.name = scannerName;
        

        int countx = 0;
        int countz = 0;

        for(int i = -10; i < 11; i++)
        {
            countx = 0;
            for(int j = -10; j < 11; j++)
            {
                
                if(data.rows[countz].row[countx])
                {
                    BoxCollider box = newActionScanner.AddComponent<BoxCollider>();
                    box.size = new Vector3(0.5f,0.5f,0.5f);
                    box.center = new Vector3(1.5f * j, 0, -1.5f * i);
                    box.isTrigger = true;
                }
                
                countx++;
            }
            countz++;
        }
        newActionScanner.transform.position = new Vector3(0,0,0);


    }

    public void Clear()
    {
        for(int i = 0; i < 21; i ++)
        {
            for(int j = 0; j < 21; j++)
            {
                data.rows[i].row[j] = false;
            }
        }
    }
}
