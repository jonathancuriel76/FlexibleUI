using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUIEditor
{
    public static class FlexibleUITools
    {
        [MenuItem("GameObject/UI/FlexibleUI/FlexibleUI Image", false, 10)]
        private static void CreateFlexibleUIImage(MenuCommand menuCommand)
        {
            var go = new GameObject("Image");
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
            go.AddComponent<FlexibleUIImage>();
        }

        [MenuItem("GameObject/UI/FlexibleUI/FlexibleUI TextMeshPro", false, 10)]
        private static void CreateFlexibleUITextMeshPro(MenuCommand menuCommand)
        {
            var go = new GameObject("Text");
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
            go.AddComponent<FlexibleUITextMeshPro>();
        }
    }
}
