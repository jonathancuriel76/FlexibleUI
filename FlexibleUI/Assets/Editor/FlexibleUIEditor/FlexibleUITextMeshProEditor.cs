using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUITextMeshPro))]
    [CanEditMultipleObjects]
    public class FlexibleUITextMeshProEditor : FlexibleUIEditor
    {
        private SerializedProperty _textData;

        private void OnEnable()
        {
            _textData = serializedObject.FindProperty("tmpData");

            // initialize _Buttons array
            var text = GetInspected<FlexibleUITextMeshPro>();
            // Call OnSkinUI() for each inspected object
            foreach (var t in text)
            {
                t.OnSkinUI();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((FlexibleUITextMeshPro)target), typeof(FlexibleUITextMeshPro), false);
            GUI.enabled = true;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_textData);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();

                // get inspected objects from editor
                var text = GetInspected<FlexibleUITextMeshPro>();
                // call OnSkinUI() for each inspected object;
                foreach (var t in text)
                {
                    t.OnSkinUI();
                }
            }
        }
    }
}