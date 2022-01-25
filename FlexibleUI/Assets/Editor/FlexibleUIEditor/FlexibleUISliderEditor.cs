using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUISlider))]
    [CanEditMultipleObjects]
    public class FlexibleUISliderEditor : FlexibleUIEditor
    {
        private SerializedProperty _sliderData;

        private void OnEnable()
        {
            _sliderData = serializedObject.FindProperty("sliderData");

            // Get all inspected objects
            // var objects = targets;
            // initialize _Buttons array
            var sliders = GetInspected<FlexibleUISlider>();
            // Call OnSkinUI() for each inspected object
            foreach (var t in sliders)
            {
                t.OnSkinUI();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((FlexibleUISlider)target), typeof(FlexibleUISlider), false);
            GUI.enabled = true;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_sliderData);
            if (!EditorGUI.EndChangeCheck()) return;
            serializedObject.ApplyModifiedProperties();

            // get inspected objects from editor
            var sliders = GetInspected<FlexibleUISlider>();
            // Call OnSkinUI() for each inspected object
            foreach (var t in sliders)
            {
                t.OnSkinUI();
            }
        }
    }
}
