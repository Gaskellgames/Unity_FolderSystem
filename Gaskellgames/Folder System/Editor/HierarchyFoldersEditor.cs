#if UNITY_EDITOR
using Gaskellgames;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Code updated by Gaskellgames
/// </summary>

namespace Gaskellgames.FolderSystem
{
    [CustomEditor(typeof(HierarchyFolders))][CanEditMultipleObjects]
    public class HierarchyFoldersEditor : Editor
    {
        #region Serialized Properties / OnEnable

        SerializedProperty customText;
        SerializedProperty customIcon;
        SerializedProperty customBackground;
        
        SerializedProperty textColor;
        SerializedProperty iconColor;
        SerializedProperty backgroundColor;
        SerializedProperty textStyle;

        private void OnEnable()
        {
            customText = serializedObject.FindProperty("customText");
            customIcon = serializedObject.FindProperty("customIcon");
            customBackground = serializedObject.FindProperty("customBackground");
            
            textColor = serializedObject.FindProperty("textColor");
            iconColor = serializedObject.FindProperty("iconColor");
            backgroundColor = serializedObject.FindProperty("backgroundColor");
            textStyle = serializedObject.FindProperty("textStyle");
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region OnInspectorGUI

        public override void OnInspectorGUI()
        {
            // get & update references
            HierarchyFolders hierarchyFolders = (HierarchyFolders)target;
            serializedObject.Update();

            // banner
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Gaskellgames/Folder System/Editor/Icons/InspectorBanner_FolderSystem.png", typeof(Texture));
            GUILayout.Box(banner, GUILayout.ExpandWidth(true), GUILayout.Height(Screen.width / 7.5f));

            // custom inspector
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Custom Colors");
            GUIContent textLabel = new GUIContent("Text", "Custom text color");
            GUIContent iconLabel = new GUIContent("Icon", "Custom icon color");
            GUIContent backgroundLabel = new GUIContent("Background", "Custom background color");
            customText.boolValue = EditorGUILayout.ToggleLeft(textLabel, customText.boolValue, GUILayout.Width(55), GUILayout.ExpandWidth(false));
            customIcon.boolValue = EditorGUILayout.ToggleLeft(iconLabel, customIcon.boolValue, GUILayout.Width(55), GUILayout.ExpandWidth(false));
            customBackground.boolValue = EditorGUILayout.ToggleLeft(backgroundLabel, customBackground.boolValue, GUILayout.Width(100), GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(textStyle);
            if (customText.boolValue)
            {
                EditorGUILayout.PropertyField(textColor);
            }
            if (customIcon.boolValue)
            {
                EditorGUILayout.PropertyField(iconColor);
            }
            if (customBackground.boolValue)
            {
                EditorGUILayout.PropertyField(backgroundColor);
            }

            // apply reference changes
            serializedObject.ApplyModifiedProperties();
        }

    #endregion

    } // class end
}
        
#endif