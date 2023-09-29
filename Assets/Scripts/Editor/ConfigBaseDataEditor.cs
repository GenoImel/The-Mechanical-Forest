using Akashic.ScriptableObjects.Scripts.ConfigBase;
using UnityEditor;

namespace Akashic.Editor
{
    [CustomEditor(typeof(ConfigBaseData))]
    internal sealed class ConfigBaseDataEditor : UnityEditor.Editor
    {
        private SerializedProperty parentSaveFolderNameProp;
        SerializedProperty saveFolderNamesProp;
        SerializedProperty saveFileNamesProp;

        void OnEnable()
        {
            parentSaveFolderNameProp = serializedObject.FindProperty("parentSaveFolderName");
            saveFolderNamesProp = serializedObject.FindProperty("saveFolderNames");
            saveFileNamesProp = serializedObject.FindProperty("saveFileNames");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
        
            EditorGUI.BeginChangeCheck();
        
            EditorGUILayout.PropertyField(parentSaveFolderNameProp, true);
            EditorGUILayout.PropertyField(saveFolderNamesProp, true);
            EditorGUILayout.PropertyField(saveFileNamesProp, true);
        
            if (EditorGUI.EndChangeCheck())
            {
                while (saveFolderNamesProp.arraySize > 3)
                {
                    saveFolderNamesProp.DeleteArrayElementAtIndex(saveFolderNamesProp.arraySize - 1);
                }
            
                while (saveFileNamesProp.arraySize > 3)
                {
                    saveFileNamesProp.DeleteArrayElementAtIndex(saveFileNamesProp.arraySize - 1);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

