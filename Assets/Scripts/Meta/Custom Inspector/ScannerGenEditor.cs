#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScannerGen))]
public class ScannerGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ScannerGen scannerGen = (ScannerGen)target;

        if(GUILayout.Button("Generate"))
        {
            scannerGen.Generate();
        }

        if(GUILayout.Button("Clear"))
        {
            scannerGen.Clear();
        }
    }
}
#endif
