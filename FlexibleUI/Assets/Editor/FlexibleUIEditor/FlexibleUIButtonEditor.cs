using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUIButton))]
    [CanEditMultipleObjects]
    public class FlexibleUIButtonEditor : FlexibleUIEditor
    {
        private SerializedProperty _buttonData;

        private void OnEnable()
        {
            _buttonData = serializedObject.FindProperty("buttonData");

            // initialize _Buttons array
            var buttons = GetInspected<FlexibleUIButton>();
            // Call OnSkinUI() for each inspected object
            foreach (var t in buttons)
            {
                t.OnSkinUI();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((FlexibleUIButton)target), typeof(FlexibleUIButton), false);
            GUI.enabled = true;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_buttonData);
            if (!EditorGUI.EndChangeCheck()) return;
            serializedObject.ApplyModifiedProperties();

            // get inspected objects from editor
            var buttons = GetInspected<FlexibleUIButton>();
            // call OnSkinUI() for each inspected object;
            foreach (var t in buttons)
            {
                t.OnSkinUI();
            }
        }
    }
}
