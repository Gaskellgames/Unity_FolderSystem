#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// Code created by Gaskellgames
/// </summary>

namespace Gaskellgames
{
    public class GaskellgamesMenuItem
    {
        #region Tools Menu
        
        protected const string GaskellgamesToolsMenu = "Tools/Gaskellgames";

        #endregion
        
        //----------------------------------------------------------------------------------------------------

        #region GameObject Menu

        protected const string GaskellgamesGameObjectMenu = "GameObject/Gaskellgames";
        
        #endregion
        
        //----------------------------------------------------------------------------------------------------

        #region Helper Functions

        protected static GameObject SetupMenuItemInContext(MenuCommand menuCommand, string gameObjectName)
        {
            // create a custom gameObject, register in the undo system, parent and set position relative to context
            GameObject go = new GameObject(gameObjectName);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            GameObject context = (GameObject)menuCommand.context;
            if(context != null) { go.transform.SetParent(context.transform); }
            go.transform.localPosition = Vector3.zero;

            return go;
        }

        #endregion
        
    } // class end
}
#endif