using UnityEditor;
using Akashic.ScriptableObjects.Scripts.ConfigBase;

[CustomEditor(typeof(ConfigBaseData))]
internal sealed class ConfigBaseDataEditor : UnityEditor.Editor
{
    private SerializedProperty saveFolderParentName;
    SerializedProperty saveSlotFolderNamesProp;
    SerializedProperty saveSlotFileNamesProp;

    void OnEnable()
    {
        saveFolderParentName = serializedObject.FindProperty("saveFolderParentName");
        saveSlotFolderNamesProp = serializedObject.FindProperty("saveSlotFolderNames");
        saveSlotFileNamesProp = serializedObject.FindProperty("saveSlotFileNames");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUI.BeginChangeCheck();
        
        EditorGUILayout.PropertyField(saveFolderParentName, true);
        EditorGUILayout.PropertyField(saveSlotFolderNamesProp, true);
        EditorGUILayout.PropertyField(saveSlotFileNamesProp, true);
        
        if (EditorGUI.EndChangeCheck())
        {
            while (saveSlotFolderNamesProp.arraySize > 3)
            {
                saveSlotFolderNamesProp.DeleteArrayElementAtIndex(saveSlotFolderNamesProp.arraySize - 1);
            }
            
            while (saveSlotFileNamesProp.arraySize > 3)
            {
                saveSlotFileNamesProp.DeleteArrayElementAtIndex(saveSlotFileNamesProp.arraySize - 1);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}

