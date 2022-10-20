using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(MakeArenaArray))]
public class MapSaveLoadEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MakeArenaArray script = (MakeArenaArray)target;
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
