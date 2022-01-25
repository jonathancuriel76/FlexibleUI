using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUIImage))]
    [CanEditMultipleObjects]
    public class FlexibleUIImageEditor : FlexibleUIEditor
    {
        private SerializedProperty _imageData;

        private void OnEnable()
        {
            _imageData = serializedObject.FindProperty("imageData");

            // Get all inspected objects
            // var objects = targets;
            // initialize _Buttons array
            var buttons = GetInspected<FlexibleUIImage>();
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
            EditorGUILayout.ObjectField("Script",
                MonoScript.FromMonoBehaviour((FlexibleUIImage)target),
                typeof(FlexibleUIImage), false);
            GUI.enabled = true;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_imageData);
            if (!EditorGUI.EndChangeCheck()) return;
            serializedObject.ApplyModifiedProperties();

            // initialize _Buttons array
            var buttons = GetInspected<FlexibleUIImage>();
            // Call OnSkinUI() for each inspected object
            foreach (var t in buttons)
            {
                t.OnSkinUI();
            }
        }
    }
}
