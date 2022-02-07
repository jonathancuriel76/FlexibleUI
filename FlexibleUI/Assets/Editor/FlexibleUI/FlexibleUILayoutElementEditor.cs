using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUI
{
    [CustomEditor(typeof(FlexibleUILayoutElement))]
    [CanEditMultipleObjects]
    public class FlexibleUILayoutElementEditor : FlexibleUIEditor
    {
        private SerializedProperty _layoutElementData;
        private Object[] _objects;

        private void OnEnable()
        {
            _layoutElementData = serializedObject.FindProperty("layoutElementData");

            // Get all inspected objects
            _objects = targets;
            // initialize _Buttons array
            var layoutElements = GetInspected<FlexibleUILayoutElement>();
            // Call OnSkinUI() for each inspected object
            foreach (var t in layoutElements)
            {
                t.OnSkinUI();
            }
        }

        public override void OnInspectorGUI()
        {
            // grab the object this inspector is editing
            //var layoutElement = (FlexibleUILayoutElement)target;

            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((FlexibleUILayoutElement)target), typeof(FlexibleUILayoutElement), false);
            GUI.enabled = true;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_layoutElementData);
            if (!EditorGUI.EndChangeCheck()) return;
            serializedObject.ApplyModifiedProperties();
            // initialize _Buttons array
            var layoutElements = GetInspected<FlexibleUILayoutElement>();
            // Call OnSkinUI() for each inspected object
            for (var i = 0; i < layoutElements.Length; i++)
            {
                layoutElements[i].OnSkinUI();
                EditorUtility.SetDirty(_objects[i]);
            }
        }
    }
}
