using System;
using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine.UI;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUIButtonData))]
    [CanEditMultipleObjects]
    public sealed class FlexibleUIButtonDataEditor : UnityEditor.Editor
    {
        private SerializedProperty _mTransition;
        private SerializedProperty _mColors;
        private SerializedProperty _mSpriteState;
        private SerializedProperty _mAnimationTriggers;
        private SerializedProperty _mNavigationMode;

        private FlexibleUIButtonData _buttonData;

        private void OnEnable()
        {
            _mTransition = serializedObject.FindProperty("transition");
            _mColors = serializedObject.FindProperty("colors");
            _mSpriteState = serializedObject.FindProperty("spriteState");
            _mAnimationTriggers = serializedObject.FindProperty("animationTriggers");
            _mNavigationMode = serializedObject.FindProperty("navigationMode");

            _buttonData = target as FlexibleUIButtonData;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Button Data", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_mTransition);

            EditorGUI.indentLevel++;

            switch (_buttonData.transition)
            {
                case Selectable.Transition.None:
                    break;
                case Selectable.Transition.ColorTint:
                    EditorGUILayout.PropertyField(_mColors);
                    break;
                case Selectable.Transition.SpriteSwap:
                    EditorGUILayout.PropertyField(_mSpriteState);
                    break;
                case Selectable.Transition.Animation:
                    EditorGUILayout.PropertyField(_mAnimationTriggers);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            EditorGUI.indentLevel--;

            EditorGUILayout.PropertyField(_mNavigationMode);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
