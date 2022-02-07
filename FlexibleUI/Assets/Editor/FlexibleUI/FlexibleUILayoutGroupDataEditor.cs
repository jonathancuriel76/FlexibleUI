using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUI
{
    [CustomEditor(typeof(FlexibleUILayoutGroupData))]
    [CanEditMultipleObjects]
    public class FlexibleUILayoutGroupDataEditor : UnityEditor.Editor
    {
        private SerializedProperty _padding;
        private SerializedProperty _spacing;
        private SerializedProperty _childAlignment;
        private SerializedProperty _childControlWidth;
        private SerializedProperty _childControlHeight;
        private SerializedProperty _childForceExpandWidth;
        private SerializedProperty _childForceExpandHeight;

        private void OnEnable()
        {
            _padding = serializedObject.FindProperty("padding");
            _spacing = serializedObject.FindProperty("spacing");
            _childAlignment = serializedObject.FindProperty("childAlignment");
            _childControlWidth = serializedObject.FindProperty("childControlWidth");
            _childControlHeight = serializedObject.FindProperty("childControlHeight");
            _childForceExpandWidth = serializedObject.FindProperty("childForceExpandWidth");
            _childForceExpandHeight = serializedObject.FindProperty("childForceExpandHeight");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Layout Group Data", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_padding, true);
            EditorGUILayout.PropertyField(_spacing, true);
            EditorGUILayout.PropertyField(_childAlignment, true);
            var rect = EditorGUILayout.GetControlRect();
            rect = EditorGUI.PrefixLabel(rect, -1, new GUIContent("Child Controls Size"));
            rect.width = Mathf.Max(50, (rect.width - 4) / 3);
            EditorGUIUtility.labelWidth = 50;
            ToggleLeft(rect, _childControlWidth, new GUIContent("Width"));
            rect.x += rect.width + 2;
            ToggleLeft(rect, _childControlHeight, new GUIContent("Height"));
            EditorGUIUtility.labelWidth = 0;

            rect = EditorGUILayout.GetControlRect();
            rect = EditorGUI.PrefixLabel(rect, -1, new GUIContent("Child Force Expand"));
            rect.width = Mathf.Max(50, (rect.width - 4) / 3);
            EditorGUIUtility.labelWidth = 50;
            ToggleLeft(rect, _childForceExpandWidth, new GUIContent("Width"));
            rect.x += rect.width + 2;
            ToggleLeft(rect, _childForceExpandHeight, new GUIContent("Height"));
            EditorGUIUtility.labelWidth = 0;

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private static void ToggleLeft(Rect position, SerializedProperty property, GUIContent label)
        {
            var toggle = property.boolValue;
            EditorGUI.showMixedValue = property.hasMultipleDifferentValues;
            EditorGUI.BeginChangeCheck();
            var oldIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            toggle = EditorGUI.ToggleLeft(position, label, toggle);
            EditorGUI.indentLevel = oldIndent;
            if (EditorGUI.EndChangeCheck())
            {
                property.boolValue = property.hasMultipleDifferentValues || !property.boolValue;
            }
            EditorGUI.showMixedValue = false;
        }
    }
}
