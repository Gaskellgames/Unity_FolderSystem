#if UNITY_EDITOR
using UnityEngine;

/// <summary>
/// Code updated by Gaskellgames
/// </summary>

namespace Gaskellgames.FolderSystem
{
    public class HierarchyFolders : MonoBehaviour
    {
        #region Variables
        
        [SerializeField]
        public bool customText;
        
        [SerializeField]
        public bool customIcon;
        
        [SerializeField]
        public bool customBackground;
        
        [SerializeField]
        public Color32 textColor = InspectorUtility.textNormalColor;
        
        [SerializeField]
        public Color32 iconColor = InspectorUtility.textNormalColor;
        
        [SerializeField]
        public Color32 backgroundColor = InspectorUtility.backgroundNormalColorLight;
        
        [SerializeField]
        public FontStyle textStyle = FontStyle.BoldAndItalic;

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region EditorLoop

        private void OnValidate()
        {
            // remove all other components
            foreach (Component component in gameObject.GetComponents<Component>())
            {
                if ( !(component is Transform || component is HierarchyFolders) )
                {
                    DestroyImmediate(component);
                    Debug.Log(component.name + "destroyed: Folders cannot contain other components.");
                }
            }
        }

        #endregion
        
    } // class end
}
        
#endif
