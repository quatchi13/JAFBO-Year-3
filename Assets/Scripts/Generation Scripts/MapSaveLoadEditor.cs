using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(SaveLoadTerrain))]
public class MapSaveLoadEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SaveLoadTerrain script = (SaveLoadTerrain)target;
        if(GUILayout.Button("Save Map"))
        {
            script.EditorSaveMap();
        }

        
        if(GUILayout.Button("Load Map"))
        {
            script.EditorLoadMap();
        }
    }
}
