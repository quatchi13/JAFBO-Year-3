using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StatAssignment))]
public class StatAssignmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        StatAssignment script = (StatAssignment)target;

        if(GUILayout.Button("Load Stats"))
        {
            script.LoadStats();
        }
    }
}
