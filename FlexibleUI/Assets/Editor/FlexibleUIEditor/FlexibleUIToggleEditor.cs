using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUIToggle))]
    [CanEditMultipleObjects]
    public class FlexibleUIToggleEditor : FlexibleUIEditor
    {
        private SerializedProperty _toggleData;

        private void OnEnable()
        {
            _toggleData = serializedObject.FindProperty("toggleData");

            // Get all inspected objects
            //var objects = targets;
            // initialize _Buttons array
            var toggles = GetInspected<FlexibleUIToggle>();
            // Call OnSkinUI() for each inspected object
            foreach (var t in toggles)
            {
                t.OnSkinUI();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((FlexibleUIToggle)target), typeof(FlexibleUIToggle), false);
            GUI.enabled = true;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_toggleData);
            if (!EditorGUI.EndChangeCheck()) return;
            serializedObject.ApplyModifiedProperties();

            // get inspected objects from editor
            var toggles = GetInspected<FlexibleUIToggle>();
            // call OnSkinUI() for each inspected object;
            foreach (var t in toggles)
            {
                t.OnSkinUI();
            }
        }
    }
}
