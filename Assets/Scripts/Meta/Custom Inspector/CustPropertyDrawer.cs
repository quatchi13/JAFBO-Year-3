using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ArrayLayout))]
public class CustPropertyDrawer : PropertyDrawer
{
    [SerializeField]
    private ScannerGen script;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

       EditorGUI.PrefixLabel(position,label);
       Rect newPosition = position;
       newPosition.y += 18f;
       SerializedProperty data = property.FindPropertyRelative("rows");
       
        
        if(data.arraySize != 21)
        {
            data.arraySize = 21;
        }

        for(int j=0; j <21; j++)
        {
                                        
        SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("row");
        newPosition.height = 18f;
        
        if(row.arraySize != 21)
        {
            row.arraySize = 21;
        }

        newPosition.width = position.width /21;


        for (int i = 0; i<21; i++)
        {
            EditorGUI.PropertyField(newPosition,row.GetArrayElementAtIndex(i),GUIContent.none);
            
            newPosition.x += newPosition.width;
        }

        newPosition.x = position.x;
        newPosition.y += 18f;
        }


       
    
        
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18f * 22;
    }
}
