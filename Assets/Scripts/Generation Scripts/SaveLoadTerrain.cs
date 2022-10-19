using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices;
using Assets.Scripts.Generation_Tools;


public class SaveLoadTerrain : MonoBehaviour
{

    public MakeArenaArray arenaRef;

    public string saveFile;

    public string loadFile;

    [DllImport("MapSaver")] public static extern void OpenFile(string address, bool isNewFile);
    [DllImport("MapSaver")] public static extern void WriteLine(string line);
    [DllImport("MapSaver")] public static extern void WriteInt(int line);
    [DllImport("MapSaver")] public static extern void CloseFile();

    [DllImport("MapSaver")] public static extern void DeleteSaveFile(string address);

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            SaveCurrentMap(arenaRef.arena, saveFile);
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        { 
            LoadMap(GenerateNewFilePath(loadFile));
        }
    }

    string GenerateNewFilePath(string fileName)
    {
        string root = Application.persistentDataPath;
        root = root + "/";
        string fullPath = root + fileName;
        fullPath = fullPath + ".txt";

        return fullPath;
    }

    public void SaveCurrentMap(Space2D currentMap, string saveName)
    {
        string filePath = GenerateNewFilePath(saveName);

        if (File.Exists(filePath)) DeleteSaveFile(filePath);

        OpenFile(filePath, true);
        WriteLine(saveName + '\n');
        WriteInt(currentMap.area());
        for(int i = 0; i < currentMap.height; i++)
        {
            for(int j = 0; j < currentMap.width; j++)
            {
                WriteLine("\n");
                WriteInt(currentMap.GetCell(new Coord(j, i)));
            }
        }
        CloseFile();
    }

    public void LoadMap(string filePath)
    {
        if (File.Exists(filePath))
        {   
            arenaRef.ResetSpace2D();
            
            string[] data = File.ReadAllLines(filePath);
            int totalLength = int.Parse(data[1]);
            int dim = (int) Mathf.Sqrt(totalLength);
            
            for(int i = 0; i < dim; i++)
            {
                for(int j = 0; j < dim; j++)
                {
                    arenaRef.arena.SetCellVal(new Coord(j, i), int.Parse(data[((j + 2) + (i * dim))]));
                }
            }

            arenaRef.DisplaySpace2D();

            arenaRef.OhNo = false;

        }
    }

    public void EditorLoadMap()
    {
        LoadMap(GenerateNewFilePath(loadFile));
    }

    public void EditorSaveMap()
    {
        SaveCurrentMap(arenaRef.arena, saveFile);
    }


}
