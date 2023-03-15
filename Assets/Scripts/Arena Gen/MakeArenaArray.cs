using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAFprocedural;
using UnityEngine.UI;
using TMPro;
using MapSaving;
using CommandPattern;
using ArenaGenerators;
using StatePattern;





public class MakeArenaArray : MonoBehaviour
{
    [SerializeField]
    private FactoryChild factory;
    private Invoker invoker;
    private ArenaGenerator arenaStateContext;

    public ArenaData arena;
    public Transform boardOrigin;
    public string saveFile;
    public string loadFile;

    public bool isWestern = false;
    private bool prev = false;

    // Start is called before the first frame update
    void Start()
    {
        invoker = new Invoker();
        arena = new ArenaData();
        arenaStateContext = new ArenaGenerator();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GenerateNewTerrain();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            invoker.Undo();
            DisplaySpace2D();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            invoker.Redo();
            DisplaySpace2D();
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            SaveToFile(saveFile);
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            LoadFromFile(loadFile);
        }

        if (prev != isWestern)
        {
            if (isWestern) SetToWestern();
            else SetToDefault();

            prev = isWestern;
        }
    }

    public void GenerateNewTerrain()
    {
        Space2D source = new Space2D(30, 30);
        arenaStateContext.Generate(source);

        ICommand command = new GenerationCommand(source, arena);
        invoker.Execute(command);

        DisplaySpace2D();
    }

    public void LoadFromFile(string filePath)
    {
        Space2D source = SaveLoadTerrain.LoadMap(SaveLoadTerrain.GenerateNewFilePath(filePath));

        ICommand command = new GenerationCommand(source, arena);
        invoker.Execute(command);

        DisplaySpace2D();
    }

    public void SaveToFile(string fileName)
    {
        print(arena.terrainValues.GetCell(new Coord(15, 10)));
        SaveLoadTerrain.SaveCurrentMap(arena.terrainValues, fileName);
    }

    

    public void DisplaySpace2D()
    {

        Infanticide(boardOrigin.transform);
        arenaStateContext.Display(arena, factory, boardOrigin);
        
    }

    protected void Infanticide(Transform parent)
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }



    public void EditorLoadMap()
    {
        LoadFromFile(loadFile);
    }

    public void EditorSaveMap()
    {
        SaveToFile(saveFile);
    }



    public void SetToWestern()
    {
        if (arenaStateContext.agState is not WesternState)
        {
            arenaStateContext.agState = new WesternState();
        }
    }

    public void SetToDefault()
    {
        if (arenaStateContext.agState is not DefaultState)
        {
            arenaStateContext.agState = new DefaultState();
        }
    }
}
