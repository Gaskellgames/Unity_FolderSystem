#if UNITY_EDITOR
using Gaskellgames;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Code updated by Gaskellgames
/// </summary>

namespace Gaskellgames.FolderSystem
{
    [InitializeOnLoad]
    public class HierarchyIcon_Folders
    {
        #region Static Variables

        // icons
        private static string assetPath = "Assets/Gaskellgames/Folder System/Editor/Icons/";
        private static Texture2D icon_FolderActiveEmpty;
        private static Texture2D icon_FolderActiveFull;
        private static Texture2D icon_FolderDisabledEmpty;
        private static Texture2D icon_FolderDisabledFull;

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region Contructors

        static HierarchyIcon_Folders()
        {
            CreateHierarchyIcon_Folder();
            
            // subscribe to inspector updates
            EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon_Folder;
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------

        #region Private Functions

        private static void CreateHierarchyIcon_Folder()
        {
            // load base icons
            Texture2D icon_FolderFull = AssetDatabase.LoadAssetAtPath(assetPath + "Icon_FolderFull.png", typeof(Texture2D)) as Texture2D;
            Texture2D icon_FolderEmpty = AssetDatabase.LoadAssetAtPath(assetPath + "Icon_FolderEmpty.png", typeof(Texture2D)) as Texture2D;
            
            // create custom icons
            icon_FolderActiveFull = InspectorUtility.TintTexture(icon_FolderFull, InspectorUtility.textNormalColor);
            icon_FolderDisabledFull = InspectorUtility.TintTexture(icon_FolderFull, InspectorUtility.textDisabledColor);
            icon_FolderActiveEmpty = InspectorUtility.TintTexture(icon_FolderEmpty, InspectorUtility.textNormalColor);
            icon_FolderDisabledEmpty = InspectorUtility.TintTexture(icon_FolderEmpty, InspectorUtility.textDisabledColor);
            
            Debug.Log("Folder System: hierarchy folder icons rebuilt.");
        }
        
        private static void DrawHierarchyIcon_Folder(int instanceID, Rect position)
        {
            // check for valid draw
            if (Event.current.type != EventType.Repaint) { return; }

            // rebuild if textures null
            if (!icon_FolderActiveEmpty || !icon_FolderActiveFull || !icon_FolderDisabledEmpty || !icon_FolderDisabledFull)
            {
                CreateHierarchyIcon_Folder();
            }
            
            GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject != null)
            {
                HierarchyFolders component = gameObject.GetComponent<HierarchyFolders>();
                if (component != null)
                {
                    // cache values
                    bool isSelected = Selection.instanceIDs.Contains(instanceID);
                    bool isActive = component.isActiveAndEnabled;
                    bool isEmpty = (0 == component.transform.childCount);
                    Color32 textColor;
                    Color32 backgroundColor;
                    Texture2D icon_Folder;
                    
                    if (isActive || isSelected)
                    {
                        // text
                        if (component.customText) { textColor = component.textColor; }
                        else { textColor = InspectorUtility.textNormalColor; }
                        
                        // icon
                        if (isEmpty)
                        {
                            if (component.customIcon) { icon_Folder = InspectorUtility.TintTexture(icon_FolderActiveEmpty, component.iconColor); }
                            else { icon_Folder = icon_FolderActiveEmpty; }
                        }
                        else
                        {
                            if (component.customIcon) { icon_Folder = InspectorUtility.TintTexture(icon_FolderActiveFull, component.iconColor); }
                            else { icon_Folder = icon_FolderActiveFull; }
                        }
                    }
                    else
                    {
                        // text
                        if (component.customText) { textColor = (Color)component.textColor * 0.6f; }
                        else { textColor = InspectorUtility.textDisabledColor; }
                        
                        // icon
                        if (isEmpty)
                        {
                            if (component.customIcon) { icon_Folder = InspectorUtility.TintTexture(icon_FolderDisabledEmpty, component.iconColor); }
                            else { icon_Folder = icon_FolderDisabledEmpty; }
                        }
                        else
                        {
                            if (component.customIcon) { icon_Folder = InspectorUtility.TintTexture(icon_FolderDisabledFull, component.iconColor); }
                            else { icon_Folder = icon_FolderDisabledFull; }
                        }
                    }

                    // get background color
                    if (isSelected)
                    {
                        if (component.customBackground) { backgroundColor = InspectorUtility.backgroundActiveColor; }
                        else { backgroundColor = InspectorUtility.backgroundActiveColor; }
                    }
                    else
                    {
                        if (component.customBackground) { backgroundColor = component.backgroundColor; }
                        else { backgroundColor = InspectorUtility.backgroundNormalColorLight; }
                    }
                    
                    // draw background
                    float hierarchyPixelHeight = 16;
                    Rect backgroundPosition = new Rect(position.xMin, position.yMin, position.width + hierarchyPixelHeight, position.height);
                    EditorGUI.DrawRect(backgroundPosition, backgroundColor);
                    
                    // draw icon
                    if(icon_Folder != null)
                    {
                        EditorGUIUtility.SetIconSize(new Vector2(hierarchyPixelHeight, hierarchyPixelHeight));
                        Rect iconPosition = new Rect(position.xMin, position.yMin, hierarchyPixelHeight, hierarchyPixelHeight);
                        GUIContent iconGUIContent = new GUIContent(icon_Folder);
                        EditorGUI.LabelField(iconPosition, iconGUIContent);
                    }
                    
                    // draw text
                    FontStyle TextStyle = component.textStyle;
                    GUIStyle hierarchyText = new GUIStyle() { };
                    hierarchyText.normal = new GUIStyleState() { textColor = textColor };
                    hierarchyText.fontStyle = TextStyle;
                    Rect textOffset = new Rect(position.xMin + hierarchyPixelHeight + 2f, position.yMin, position.width, position.height);
                    EditorGUI.LabelField(textOffset, component.name, hierarchyText);
                }
            }
        }

        #endregion
        
    } // class end
}

#endif