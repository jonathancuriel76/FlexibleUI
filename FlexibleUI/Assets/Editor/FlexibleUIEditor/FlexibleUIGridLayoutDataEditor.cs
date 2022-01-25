using Assets.Scripts.FlexibleUI;
using UnityEditor;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUIGridLayoutGroupData))]
    [CanEditMultipleObjects]
    public class FlexibleUIGridLayoutDataEditor : UnityEditor.Editor
    {
        private SerializedProperty _padding;
        private SerializedProperty _cellSize;
        private SerializedProperty _spacing;
        private SerializedProperty _startCorner;
        private SerializedProperty _startAxis;
        private SerializedProperty _childAlignment;
        private SerializedProperty _constraint;
        private SerializedProperty _constraintCount;

        private void OnEnable()
        {
            _padding = serializedObject.FindProperty("padding");
            _cellSize = serializedObject.FindProperty("cellSize");
            _spacing = serializedObject.FindProperty("spacing");
            _startCorner = serializedObject.FindProperty("startCorner");
            _startAxis = serializedObject.FindProperty("startAxis");
            _childAlignment = serializedObject.FindProperty("childAlignment");
            _constraint = serializedObject.FindProperty("constraint");
            _constraintCount = serializedObject.FindProperty("constraintCount");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_padding, true);
            EditorGUILayout.PropertyField(_cellSize, true);
            EditorGUILayout.PropertyField(_spacing, true);
            EditorGUILayout.PropertyField(_startCorner, true);
            EditorGUILayout.PropertyField(_startAxis, true);
            EditorGUILayout.PropertyField(_childAlignment, true);
            EditorGUILayout.PropertyField(_constraint, true);
            if (_constraint.enumValueIndex > 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_constraintCount, true);
                EditorGUI.indentLevel--;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
