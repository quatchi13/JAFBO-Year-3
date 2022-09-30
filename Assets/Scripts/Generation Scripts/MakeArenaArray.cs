using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Generation_Tools;
using UnityEngine.UI;
using TMPro;

public class MakeArenaArray : MonoBehaviour
{
    public Space2D arena;
    
    public List<GameObject> prefabs = new List<GameObject> { };
    public Transform boardOrigin;
    public bool OhNo = true;
    // Start is called before the first frame update
    void Start()
    {
        arena = new Space2D(30, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (OhNo)
            {
                ArenaGenFunctions_Prototype.ArenaPrototype(arena);
               
                DisplaySpace2D();
            }
            else
            {
                Space2D myEntireAss = new Space2D(28, 28);
                BasicBuilderFunctions.Flood(myEntireAss, new Cell(0), new Cell(1));
                myEntireAss.worldOrigin = new Coord(1, 1);
                BasicBuilderFunctions.CopySpaceAToB(myEntireAss, arena, new List<Cell> { });
             
                DisplaySpace2D();
            }
            OhNo = !OhNo;
        }
    }

    protected void DisplaySpace2D()
    {

        Infanticide(boardOrigin.transform);
        
        for (int i = 0; i < arena.height; i++)
        {
            for (int j = 0; j < arena.width; j++)
            {

                GameObject currentTile = Instantiate(prefabs[arena.GetCell(new Coord(j,i))], new Vector3(boardOrigin.position.x + j, 0, boardOrigin.position.z - i), Quaternion.identity);
                currentTile.transform.SetParent(boardOrigin);
                //iAmLessSad.text = iAmLessSad.text + (arena.GetCell(new Coord(j, i))) + " ";

            }
        }
    }

    protected void Infanticide(Transform parent)
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
