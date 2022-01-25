using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUILayoutElementData))]
    [CanEditMultipleObjects]
    public class FlexibleUILayoutElementDataEditor : UnityEditor.Editor
    {
        private SerializedProperty _ignoreLayout;
        private SerializedProperty _minWidth;
        private SerializedProperty _minHeight;
        private SerializedProperty _preferredWidth;
        private SerializedProperty _preferredHeight;
        private SerializedProperty _flexibleWidth;
        private SerializedProperty _flexibleHeight;
        private SerializedProperty _layoutPriority;

        private void OnEnable()
        {
            _ignoreLayout = serializedObject.FindProperty("ignoreLayout");
            _minWidth = serializedObject.FindProperty("minWidth");
            _minHeight = serializedObject.FindProperty("minHeight");
            _preferredWidth = serializedObject.FindProperty("preferredWidth");
            _preferredHeight = serializedObject.FindProperty("preferredHeight");
            _flexibleWidth = serializedObject.FindProperty("flexibleWidth");
            _flexibleHeight = serializedObject.FindProperty("flexibleHeight");
            _layoutPriority = serializedObject.FindProperty("layoutPriority");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_ignoreLayout);

            if (!_ignoreLayout.boolValue)
            {
                EditorGUILayout.Space();

                LayoutElementField(_minWidth, 0);
                LayoutElementField(_minHeight, 0);
                LayoutElementField(_preferredWidth, 0);
                LayoutElementField(_preferredHeight, 0);
                LayoutElementField(_flexibleWidth, 1);
                LayoutElementField(_flexibleHeight, 1);
            }

            EditorGUILayout.PropertyField(_layoutPriority);

            serializedObject.ApplyModifiedProperties();
        }

        private static void LayoutElementField(SerializedProperty property, float defaultValue)
        {
            var position = EditorGUILayout.GetControlRect();

            // Label
            var label = EditorGUI.BeginProperty(position, null, property);

            // Rects
            var fieldPosition = EditorGUI.PrefixLabel(position, label);

            var toggleRect = fieldPosition;
            toggleRect.width = 16;

            var floatFieldRect = fieldPosition;
            floatFieldRect.xMin += 16;

            // Checkbox
            EditorGUI.BeginChangeCheck();
            var enabled = EditorGUI.ToggleLeft(toggleRect, GUIContent.none, property.floatValue >= 0);
            if (EditorGUI.EndChangeCheck())
            {
                // This could be made better to set all of the targets to their initial width, but minimizing code change for now
                property.floatValue = enabled ? defaultValue : -1;
            }

            if (!property.hasMultipleDifferentValues && property.floatValue >= 0)
            {
                // Float field
                EditorGUIUtility.labelWidth = 4; // Small invisible label area for drag zone functionality
                EditorGUI.BeginChangeCheck();
                var newValue = EditorGUI.FloatField(floatFieldRect, new GUIContent(" "), property.floatValue);
                if (EditorGUI.EndChangeCheck())
                {
                    property.floatValue = Mathf.Max(0, newValue);
                }
                EditorGUIUtility.labelWidth = 0;
            }

            EditorGUI.EndProperty();
        }
    }
}

