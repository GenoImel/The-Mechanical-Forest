using UnityEngine;
using UnityEditor;
using Akashic.ScriptableObjects.Scripts.ConfigBase;

[CustomEditor(typeof(ConfigBaseData))]
internal sealed class ConfigBaseDataEditor : UnityEditor.Editor
{
    SerializedProperty saveSlotFileNamesProp;

    void OnEnable()
    {
        saveSlotFileNamesProp = serializedObject.FindProperty("saveSlotFileNames");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw the saveSlotFileNames list but enforce a limit
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(saveSlotFileNamesProp, true);
        if (EditorGUI.EndChangeCheck())
        {
            while (saveSlotFileNamesProp.arraySize > 3)
            {
                saveSlotFileNamesProp.DeleteArrayElementAtIndex(saveSlotFileNamesProp.arraySize - 1);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}

