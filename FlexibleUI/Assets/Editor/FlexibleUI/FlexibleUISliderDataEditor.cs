using System;
using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine.UI;

namespace Assets.Editor.FlexibleUI
{
    [CustomEditor(typeof(FlexibleUISliderData))]
    [CanEditMultipleObjects]
    public sealed class FlexibleUISliderDataEditor : UnityEditor.Editor
    {
        private SerializedProperty _transition;
        private SerializedProperty _colors;
        private SerializedProperty _spriteState;
        private SerializedProperty _animationTriggers;
        private SerializedProperty _navigation;
        private SerializedProperty _direction;
        private SerializedProperty _minValue;
        private SerializedProperty _maxValue;
        private SerializedProperty _wholeNumbers;

        private FlexibleUISliderData _sliderData;

        private void OnEnable()
        {
            _sliderData = target as FlexibleUISliderData;
            _transition = serializedObject.FindProperty("transition");
            _colors = serializedObject.FindProperty("colors");
            _spriteState = serializedObject.FindProperty("spriteState");
            _animationTriggers = serializedObject.FindProperty("animationTriggers");
            _navigation = serializedObject.FindProperty("navigationMode");
            _direction = serializedObject.FindProperty("direction");
            _minValue = serializedObject.FindProperty("minValue");
            _maxValue = serializedObject.FindProperty("maxValue");
            _wholeNumbers = serializedObject.FindProperty("wholeNumbers");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Slider Data", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_transition);

            EditorGUI.indentLevel++;

            switch (_sliderData.transition)
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
            EditorGUILayout.PropertyField(_direction);
            EditorGUILayout.PropertyField(_minValue);
            EditorGUILayout.PropertyField(_maxValue);
            EditorGUILayout.PropertyField(_wholeNumbers);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}