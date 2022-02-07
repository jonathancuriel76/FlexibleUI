using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUI
{
    [InitializeOnLoad]
    public class FlexibleUICustomHierarchy : UnityEditor.Editor
    {
        private const string IconPath = "Assets/Resources/IconFlexibleUI.png";
        private static readonly Vector2 Offset = new Vector2(18, 0);
        private static readonly Texture2D Icon;

        static FlexibleUICustomHierarchy()
        {
            Icon = AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath);
            EditorApplication.hierarchyWindowItemOnGUI -= HandleHierarchyWindowItemOnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var fontColor = new Color32(238, 173, 30, 255);

            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj == null) return;
            var flexibleUI = obj.GetComponent<Scripts.FlexibleUI.FlexibleUI>();

            if (flexibleUI == null || Event.current.type != EventType.Repaint) return;
            var offsetRect = new Rect(selectionRect.position + Offset, selectionRect.size);

            EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = fontColor }
            });

            if (Icon == null)
            {
                return;
            }
            else
            {
                const float iconWidth = 20;
                EditorGUIUtility.SetIconSize(new Vector2(iconWidth, iconWidth));
                var padding = new Vector2(5, 0);
                var iconDrawRect = new Rect(
                    selectionRect.xMax - (iconWidth + padding.x),
                    selectionRect.yMin,
                    selectionRect.width,
                    selectionRect.height);
                var iconGUIContent = new GUIContent(Icon);
                EditorGUI.LabelField(iconDrawRect, iconGUIContent);
                EditorGUIUtility.SetIconSize(Vector2.zero);
            }
        }
    }
}
