using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tool))]
public class StructureEditor : Editor
{
    private SerializedProperty structurePositions;

    private void OnEnable()
    {
        structurePositions = serializedObject.FindProperty("positions");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        if (GUILayout.Button("Add Position"))
        {
            int index = structurePositions.arraySize;
            structurePositions.InsertArrayElementAtIndex(index);
            SerializedProperty position = structurePositions.GetArrayElementAtIndex(index);
            position.vector2IntValue = Vector2Int.zero;
        }

        for (int i = 0; i < structurePositions.arraySize; i++)
        {
            SerializedProperty position = structurePositions.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(position);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
