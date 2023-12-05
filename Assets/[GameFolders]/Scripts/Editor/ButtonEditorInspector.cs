using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MazeGenerator))]
public class ButtonEditorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();

        if (GUILayout.Button("StartGenerate"))
        {
            ((MazeGenerator)target).StartGenerate();
        }

        serializedObject.ApplyModifiedProperties();
    }
}