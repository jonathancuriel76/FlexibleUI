using System;
using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine.UI;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUIToggleData))]
    [CanEditMultipleObjects]
    public sealed class FlexibleUIToggleDataEditor : UnityEditor.Editor
    {
        private SerializedProperty _transition;
        private SerializedProperty _colors;
        private SerializedProperty _spriteState;
        private SerializedProperty _animationTriggers;
        private SerializedProperty _navigation;

        private SerializedProperty _toggleTransition;

        private FlexibleUIToggleData _toggleData;

        private void OnEnable()
        {
            _transition = serializedObject.FindProperty("transition");
            _colors = serializedObject.FindProperty("colors");
            _spriteState = serializedObject.FindProperty("spriteState");
            _animationTriggers = serializedObject.FindProperty("animationTriggers");
            _navigation = serializedObject.FindProperty("navigationMode");
            _toggleTransition = serializedObject.FindProperty("toggleTransition");

            _toggleData = target as FlexibleUIToggleData;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Toggle Data", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_transition);

            EditorGUI.indentLevel++;

            // Currently when switching from Color Tint to None/SpriteSwap/Animation, the color tint is retained.
            switch (_toggleData.transition)
            {
                case Selectable.Transition.None:
                    break;
                case Selectable.Transition.ColorTint:
                    EditorGUILayout.PropertyField(_colors);
                    break;
                case Selectable.Transition.SpriteSwap:

                    EditorGUILayout.PropertyField(_spriteState);
                    break;
                case Selectable.Transition.Animation:
                    EditorGUILayout.PropertyField(_animationTriggers);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            EditorGUI.indentLevel--;

            EditorGUILayout.PropertyField(_navigation);

            EditorGUILayout.PropertyField(_toggleTransition);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
