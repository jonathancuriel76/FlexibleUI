using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUILayoutGroup))]
    [CanEditMultipleObjects]
    public class FlexibleUILayoutGroupEditor : FlexibleUIEditor
    {
        private SerializedProperty _layoutGroupData;
        private Object[] _objects;

        private void OnEnable()
        {
            _layoutGroupData = serializedObject.FindProperty("layoutGroupData");

            // Get all inspected objects
            _objects = targets;
            // initialize _Buttons array
            var layoutGroups = GetInspected<FlexibleUILayoutGroup>();
            // Call OnSkinUI() for each inspected object
            foreach (var t in layoutGroups)
            {
                t.OnSkinUI();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((FlexibleUILayoutGroup)target), typeof(FlexibleUILayoutGroup), false);
            GUI.enabled = true;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_layoutGroupData);
            if (!EditorGUI.EndChangeCheck()) return;
            serializedObject.ApplyModifiedProperties();
            // initialize _Buttons array
            var layoutGroups = GetInspected<FlexibleUILayoutGroup>();
            // Call OnSkinUI() for each inspected object
            for (var i = 0; i < layoutGroups.Length; i++)
            {
                layoutGroups[i].OnSkinUI();
                EditorUtility.SetDirty(_objects[i]);
            }
        }
    }
}
