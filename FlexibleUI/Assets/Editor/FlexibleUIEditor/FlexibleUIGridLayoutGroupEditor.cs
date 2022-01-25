using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUIGridLayoutGroup))]
    [CanEditMultipleObjects]
    public class FlexibleUIGridLayoutGroupEditor : FlexibleUIEditor
    {
        private SerializedProperty _gridLayoutGroupData;
        private Object[] _objects;

        private void OnEnable()
        {
            _gridLayoutGroupData = serializedObject.FindProperty("gridLayoutGroupData");

            // Get all inspected objects
            _objects = targets;
            // initialize _Buttons array
            var gridLayoutGroups = GetInspected<FlexibleUIGridLayoutGroup>();
            // Call OnSkinUI() for each inspected object
            foreach (var t in gridLayoutGroups)
            {
                t.OnSkinUI();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((FlexibleUIGridLayoutGroup)target), typeof(FlexibleUIGridLayoutGroup), false);
            GUI.enabled = true;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_gridLayoutGroupData);
            if (!EditorGUI.EndChangeCheck()) return;
            serializedObject.ApplyModifiedProperties();
            // initialize _Buttons array
            var gridLayoutGroups = GetInspected<FlexibleUIGridLayoutGroup>();
            // Call OnSkinUI() for each inspected object
            for (var i = 0; i < gridLayoutGroups.Length; i++)
            {
                gridLayoutGroups[i].OnSkinUI();
                EditorUtility.SetDirty(_objects[i]);
            }
        }
    }
}
