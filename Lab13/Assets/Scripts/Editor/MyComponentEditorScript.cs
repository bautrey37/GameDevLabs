using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyComponent))]
public class MyComponentEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Update Object"))
        {
            Debug.Log("Buton pressed");
        }
    }
}
