using System;
using Assets.Scripts.FlexibleUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Editor.FlexibleUIEditor
{
    [CustomEditor(typeof(FlexibleUIImageData))]
    [CanEditMultipleObjects]
    public class FlexibleUIImageDataEditor : UnityEditor.Editor
    {
        private SerializedProperty _imageSprite;
        private SerializedProperty _imageColor;
        private SerializedProperty _imageMaterial;
        private SerializedProperty _imageRaycastTarget;
        private SerializedProperty _imageType;
        private SerializedProperty _imageFillMethod;
        private SerializedProperty _imageOriginHorizontal;
        private SerializedProperty _imageOriginVertical;
        private SerializedProperty _imageOrigin90;
        private SerializedProperty _imageOrigin180;
        private SerializedProperty _imageOrigin360;
        private SerializedProperty _imageClockwise;
        private SerializedProperty _imagePreserveAspect;
        private SerializedProperty _imageFillCenter;

        private FlexibleUIImageData _imageData;

        private void OnEnable()
        {
            _imageSprite = serializedObject.FindProperty("imageSprite");
            _imageColor = serializedObject.FindProperty("imageColor");
            _imageMaterial = serializedObject.FindProperty("imageMaterial");
            _imageRaycastTarget = serializedObject.FindProperty("imageRaycastTarget");
            _imageType = serializedObject.FindProperty("imageType");
            _imageFillMethod = serializedObject.FindProperty("imageFillMethod");
            _imageOriginHorizontal = serializedObject.FindProperty("imageOriginHorizontal");
            _imageOriginVertical = serializedObject.FindProperty("imageOriginVertical");
            _imageOrigin90 = serializedObject.FindProperty("imageOrigin90");
            _imageOrigin180 = serializedObject.FindProperty("imageOrigin180");
            _imageOrigin360 = serializedObject.FindProperty("imageOrigin360");
            _imageClockwise = serializedObject.FindProperty("imageClockwise");
            _imagePreserveAspect = serializedObject.FindProperty("imagePreserveAspect");
            _imageFillCenter = serializedObject.FindProperty("imageFillCenter");

            _imageData = target as FlexibleUIImageData;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Image Data", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_imageSprite);
            EditorGUILayout.PropertyField(_imageColor);
            EditorGUILayout.PropertyField(_imageMaterial);
            EditorGUILayout.PropertyField(_imageRaycastTarget);

            if (_imageSprite.objectReferenceValue != null)
            {
                EditorGUILayout.PropertyField(_imageType);
                switch (_imageData.imageType)
                {
                    case Image.Type.Simple:
                        DrawPreserveAspect();
                        break;
                    case Image.Type.Sliced:
                        if (_imageData.imageSprite.border == Vector4.zero)
                        {
                            EditorGUILayout.HelpBox("This Image doesn't have a border.", MessageType.Warning);
                        }
                        else
                        {
                            DrawFillCenter();
                        }
                        break;
                    case Image.Type.Tiled:
                        if (_imageData.imageSprite.border == Vector4.zero && (_imageData.imageSprite.texture.wrapMode != TextureWrapMode.Repeat || _imageData.imageSprite.packed))
                        {
                            EditorGUILayout.HelpBox("It looks like you want to tile a sprite with no border. It would be more efficient to modify the Sprite properties, clear the Packing tag and set the Wrap mode to Repeat.", MessageType.Warning);
                        }
                        else
                        {
                            DrawFillCenter();
                        }
                        break;
                    case Image.Type.Filled:
                        DrawFilled();
                        DrawPreserveAspect();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void DrawPreserveAspect()
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_imagePreserveAspect);
            EditorGUI.indentLevel--;
        }

        private void DrawFillCenter()
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_imageFillCenter);
            EditorGUI.indentLevel--;
        }

        private void DrawFilled()
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_imageFillMethod);
            switch (_imageData.imageFillMethod)
            {
                case Image.FillMethod.Horizontal:
                    EditorGUILayout.PropertyField(_imageOriginHorizontal);
                    break;
                case Image.FillMethod.Vertical:
                    EditorGUILayout.PropertyField(_imageOriginVertical);
                    break;
                case Image.FillMethod.Radial90:
                    EditorGUILayout.PropertyField(_imageOrigin90);
                    break;
                case Image.FillMethod.Radial180:
                    EditorGUILayout.PropertyField(_imageOrigin180);
                    break;
                case Image.FillMethod.Radial360:
                    EditorGUILayout.PropertyField(_imageOrigin360);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_imageData.imageFillMethod > Image.FillMethod.Vertical)
            {
                EditorGUILayout.PropertyField(_imageClockwise);
            }
            EditorGUI.indentLevel--;
        }
    }
}
