using System;
using System.Collections.Generic;
using Assets.Scripts.FlexibleUI;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.FlexibleUI
{
    [CustomEditor(typeof(FlexibleUITextMeshProData))]
    [CanEditMultipleObjects]
    public class FlexibleUITextMeshProDataEditor : UnityEditor.Editor
    {
        //private FlexibleUITextMeshProData _textData;
        private SerializedProperty _fontAsset;
        private SerializedProperty _sharedMaterial;
        private SerializedProperty _fontStyle;
        private SerializedProperty _fontSize;
        private SerializedProperty _fontSizeBase;
        private SerializedProperty _vertexColor;
        private SerializedProperty _useColorGradient;
        private SerializedProperty _colorMode;
        private SerializedProperty _gradientPreset;
        private SerializedProperty _topLeft;
        private SerializedProperty _topRight;
        private SerializedProperty _bottomLeft;
        private SerializedProperty _bottomRight;
        private SerializedProperty _autoSize;
        private SerializedProperty _minFontSize;
        private SerializedProperty _maxFontSize;

        [SerializeField] private Material[] materialPresets;
        [SerializeField] private GUIContent[] materialPresetNames;
        [SerializeField] private int materialPresetSelectionIndex;
        private readonly Dictionary<int, int> _materialPresetIndexLookup = new Dictionary<int, int>();

        private void OnEnable()
        {
            //_textData = target as FlexibleUITextMeshProData;
            _fontAsset = serializedObject.FindProperty("fontAsset");
            _sharedMaterial = serializedObject.FindProperty("sharedMaterial");
            _fontStyle = serializedObject.FindProperty("fontStyle");
            _fontSize = serializedObject.FindProperty("fontSize");
            _fontSizeBase = serializedObject.FindProperty("fontSizeBase");
            _autoSize = serializedObject.FindProperty("autoSize");
            _minFontSize = serializedObject.FindProperty("minFontSize");
            _maxFontSize = serializedObject.FindProperty("maxFontSize");
            _vertexColor = serializedObject.FindProperty("vertexColor");
            _useColorGradient = serializedObject.FindProperty("useColorGradient");
            _colorMode = serializedObject.FindProperty("colorMode");
            _gradientPreset = serializedObject.FindProperty("gradientPreset");
            _topLeft = serializedObject.FindProperty("topLeft");
            _topRight = serializedObject.FindProperty("topRight");
            _bottomLeft = serializedObject.FindProperty("bottomLeft");
            _bottomRight = serializedObject.FindProperty("bottomRight");

            materialPresetNames = GetMaterialPresets();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // FONT ASSET
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_fontAsset);
            if (EditorGUI.EndChangeCheck())
            {
                // If 'None' is selected, get default font asset.
                if (_fontAsset.objectReferenceValue == null)
                {
                    _fontAsset.objectReferenceValue = TMP_Settings.defaultFontAsset;
                }
                // Get new Material Presets for the new font asset
                materialPresetNames = GetMaterialPresets();
                // Switch to the default material (index = 0)
                materialPresetSelectionIndex = 0;
                // Assign the material to the Property
                _sharedMaterial.objectReferenceValue = materialPresets[materialPresetSelectionIndex];
            }

            // FONT MATERIAL PRESET
            if (materialPresetNames != null)
            {
                EditorGUI.BeginChangeCheck();
                var rect = EditorGUILayout.GetControlRect(false, 17);

                EditorGUI.BeginProperty(rect, new GUIContent("Material Preset"), _sharedMaterial);

                var oldHeight = EditorStyles.popup.fixedHeight;
                EditorStyles.popup.fixedHeight = rect.height;

                var oldSize = EditorStyles.popup.fontSize;
                EditorStyles.popup.fontSize = 11;

                if (_sharedMaterial.objectReferenceValue != null)
                    _materialPresetIndexLookup.TryGetValue(_sharedMaterial.objectReferenceValue.GetInstanceID(), out materialPresetSelectionIndex);

                materialPresetSelectionIndex = EditorGUI.Popup(rect, new GUIContent("Material Preset"), materialPresetSelectionIndex, materialPresetNames);

                EditorGUI.EndProperty();

                if (EditorGUI.EndChangeCheck())
                {
                    _sharedMaterial.objectReferenceValue = materialPresets[materialPresetSelectionIndex];
                }

                EditorStyles.popup.fixedHeight = oldHeight;
                EditorStyles.popup.fontSize = oldSize;
            }

            // FONT STYLE
            EditorGUI.BeginChangeCheck();

            int v1, v2, v3, v4, v5, v6, v7;

            if (EditorGUIUtility.wideMode)
            {
                var rect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight + 2f);

                EditorGUI.BeginProperty(rect, new GUIContent("Font Style"), _fontStyle);

                EditorGUI.PrefixLabel(rect, new GUIContent("Font Style"));

                var styleValue = _fontStyle.intValue;

                rect.x += EditorGUIUtility.labelWidth;
                rect.width -= EditorGUIUtility.labelWidth;

                rect.width = Mathf.Max(25f, rect.width / 7f);

                v1 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 1) == 1, new GUIContent("B", "Bold"), TMP_UIStyleManager.alignmentButtonLeft) ? 1 : 0; // Bold
                rect.x += rect.width;
                v2 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 2) == 2, new GUIContent("I", "Italic"), TMP_UIStyleManager.alignmentButtonMid) ? 2 : 0; // Italics
                rect.x += rect.width;
                v3 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 4) == 4, new GUIContent("U", "Underline"), TMP_UIStyleManager.alignmentButtonMid) ? 4 : 0; // Underline
                rect.x += rect.width;
                v7 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 64) == 64, new GUIContent("S", "Strikethrough"), TMP_UIStyleManager.alignmentButtonRight) ? 64 : 0; // Strikethrough
                rect.x += rect.width;

                var selected = 0;

                EditorGUI.BeginChangeCheck();
                v4 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 8) == 8, new GUIContent("ab", "Lowercase"), TMP_UIStyleManager.alignmentButtonLeft) ? 8 : 0; // Lowercase
                if (EditorGUI.EndChangeCheck() && v4 > 0)
                {
                    selected = v4;
                }
                rect.x += rect.width;
                EditorGUI.BeginChangeCheck();
                v5 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 16) == 16, new GUIContent("AB", "Uppercase"), TMP_UIStyleManager.alignmentButtonMid) ? 16 : 0; // Uppercase
                if (EditorGUI.EndChangeCheck() && v5 > 0)
                {
                    selected = v5;
                }
                rect.x += rect.width;
                EditorGUI.BeginChangeCheck();
                v6 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 32) == 32, new GUIContent("SC", "SmallCaps"), TMP_UIStyleManager.alignmentButtonRight) ? 32 : 0; // Smallcaps
                if (EditorGUI.EndChangeCheck() && v6 > 0)
                {
                    selected = v6;
                }

                if (selected > 0)
                {
                    v4 = selected == 8 ? 8 : 0;
                    v5 = selected == 16 ? 16 : 0;
                    v6 = selected == 32 ? 32 : 0;
                }

                EditorGUI.EndProperty();
            }
            else
            {
                var rect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight + 2f);

                EditorGUI.BeginProperty(rect, new GUIContent("Font Style"), _fontStyle);

                EditorGUI.PrefixLabel(rect, new GUIContent("Font Style"));

                var styleValue = _fontStyle.intValue;

                rect.x += EditorGUIUtility.labelWidth;
                rect.width -= EditorGUIUtility.labelWidth;
                rect.width = Mathf.Max(25f, rect.width / 4f);

                v1 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 1) == 1, new GUIContent("B", "Bold"), TMP_UIStyleManager.alignmentButtonLeft) ? 1 : 0; // Bold
                rect.x += rect.width;
                v2 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 2) == 2, new GUIContent("I", "Italic"), TMP_UIStyleManager.alignmentButtonMid) ? 2 : 0; // Italics
                rect.x += rect.width;
                v3 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 4) == 4, new GUIContent("U", "Underline"), TMP_UIStyleManager.alignmentButtonMid) ? 4 : 0; // Underline
                rect.x += rect.width;
                v7 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 64) == 64, new GUIContent("S", "Strikethrough"), TMP_UIStyleManager.alignmentButtonRight) ? 64 : 0; // Strikethrough

                rect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight + 2f);

                rect.x += EditorGUIUtility.labelWidth;
                rect.width -= EditorGUIUtility.labelWidth;

                rect.width = Mathf.Max(25f, rect.width / 4f);

                var selected = 0;

                EditorGUI.BeginChangeCheck();
                v4 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 8) == 8, new GUIContent("ab", "Lowercase"), TMP_UIStyleManager.alignmentButtonLeft) ? 8 : 0; // Lowercase
                if (EditorGUI.EndChangeCheck() && v4 > 0)
                {
                    selected = v4;
                }
                rect.x += rect.width;
                EditorGUI.BeginChangeCheck();
                v5 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 16) == 16, new GUIContent("AB", "Uppercase"), TMP_UIStyleManager.alignmentButtonMid) ? 16 : 0; // Uppercase
                if (EditorGUI.EndChangeCheck() && v5 > 0)
                {
                    selected = v5;
                }
                rect.x += rect.width;
                EditorGUI.BeginChangeCheck();
                v6 = TMP_EditorUtility.EditorToggle(rect, (styleValue & 32) == 32, new GUIContent("SC", "SmallCaps"), TMP_UIStyleManager.alignmentButtonRight) ? 32 : 0; // Smallcaps
                if (EditorGUI.EndChangeCheck() && v6 > 0)
                {
                    selected = v6;
                }

                if (selected > 0)
                {
                    v4 = selected == 8 ? 8 : 0;
                    v5 = selected == 16 ? 16 : 0;
                    v6 = selected == 32 ? 32 : 0;
                }

                EditorGUI.EndProperty();
            }

            if (EditorGUI.EndChangeCheck())
            {
                _fontStyle.intValue = v1 + v2 + v3 + v4 + v5 + v6 + v7;
            }

            EditorGUILayout.Space();

            // FONT COLOR
            EditorGUILayout.PropertyField(_vertexColor);
            // COLOR GRADIENT
            EditorGUILayout.PropertyField(_useColorGradient);

            if (_useColorGradient.boolValue)
            {
                EditorGUI.indentLevel++;
                // GRADIENT PRESET
                EditorGUILayout.PropertyField(_gradientPreset);

                if (_gradientPreset.objectReferenceValue == null)
                {
                    // COLOR MODE
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_colorMode);
                    if (EditorGUI.EndChangeCheck())
                    {
                        switch ((ColorMode)_colorMode.enumValueIndex)
                        {
                            case ColorMode.Single:
                                _topRight.colorValue = _topLeft.colorValue;
                                _bottomLeft.colorValue = _topLeft.colorValue;
                                _bottomRight.colorValue = _topLeft.colorValue;
                                break;
                            case ColorMode.HorizontalGradient:
                                _bottomLeft.colorValue = _topLeft.colorValue;
                                _bottomRight.colorValue = _topRight.colorValue;
                                break;
                            case ColorMode.VerticalGradient:
                                _topRight.colorValue = _topLeft.colorValue;
                                _bottomRight.colorValue = _bottomLeft.colorValue;
                                break;
                            case ColorMode.FourCornersGradient:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    // GRADIENT COLORS
                    switch ((ColorMode)_colorMode.enumValueIndex)
                    {
                        case ColorMode.Single:
                            EditorGUI.BeginChangeCheck();
                            var rect = EditorGUILayout.GetControlRect(true,
                                EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2));
                            EditorGUI.PrefixLabel(rect, new GUIContent("Colors"));
                            rect.x += EditorGUIUtility.labelWidth;
                            rect.width = (rect.width - EditorGUIUtility.labelWidth) /
                                         (EditorGUIUtility.wideMode ? 1f : 2f);
                            TMP_EditorUtility.DrawColorProperty(rect, _topLeft);
                            if (EditorGUI.EndChangeCheck())
                            {
                                _topRight.colorValue = _topLeft.colorValue;
                                _bottomLeft.colorValue = _topLeft.colorValue;
                                _bottomRight.colorValue = _topLeft.colorValue;
                            }
                            break;

                        case ColorMode.HorizontalGradient:
                            rect = EditorGUILayout.GetControlRect(true,
                                EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2));
                            EditorGUI.PrefixLabel(rect, new GUIContent("Colors"));
                            rect.x += EditorGUIUtility.labelWidth;
                            rect.width = (rect.width - EditorGUIUtility.labelWidth) / 2f;

                            EditorGUI.BeginChangeCheck();
                            TMP_EditorUtility.DrawColorProperty(rect, _topLeft);
                            if (EditorGUI.EndChangeCheck())
                            {
                                _bottomLeft.colorValue = _topLeft.colorValue;
                            }

                            rect.x += rect.width;

                            EditorGUI.BeginChangeCheck();
                            TMP_EditorUtility.DrawColorProperty(rect, _topRight);
                            if (EditorGUI.EndChangeCheck())
                            {
                                _bottomRight.colorValue = _topRight.colorValue;
                            }
                            break;

                        case ColorMode.VerticalGradient:
                            rect = EditorGUILayout.GetControlRect(false,
                                EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2));
                            EditorGUI.PrefixLabel(rect, new GUIContent("Colors"));
                            rect.x += EditorGUIUtility.labelWidth;
                            rect.width = (rect.width - EditorGUIUtility.labelWidth) /
                                         (EditorGUIUtility.wideMode ? 1f : 2f);
                            rect.height = EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2);

                            EditorGUI.BeginChangeCheck();
                            TMP_EditorUtility.DrawColorProperty(rect, _topLeft);
                            if (EditorGUI.EndChangeCheck())
                            {
                                _topRight.colorValue = _topLeft.colorValue;
                            }

                            rect = EditorGUILayout.GetControlRect(false,
                                EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2));
                            rect.x += EditorGUIUtility.labelWidth;
                            rect.width = (rect.width - EditorGUIUtility.labelWidth) /
                                         (EditorGUIUtility.wideMode ? 1f : 2f);
                            rect.height = EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2);

                            EditorGUI.BeginChangeCheck();
                            TMP_EditorUtility.DrawColorProperty(rect, _bottomLeft);
                            if (EditorGUI.EndChangeCheck())
                            {
                                _bottomRight.colorValue = _bottomLeft.colorValue;
                            }
                            break;

                        case ColorMode.FourCornersGradient:
                            rect = EditorGUILayout.GetControlRect(true,
                                EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2));
                            EditorGUI.PrefixLabel(rect, new GUIContent("Colors"));
                            rect.x += EditorGUIUtility.labelWidth;
                            rect.width = (rect.width - EditorGUIUtility.labelWidth) / 2f;
                            rect.height = EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2);

                            TMP_EditorUtility.DrawColorProperty(rect, _topLeft);
                            rect.x += rect.width;
                            TMP_EditorUtility.DrawColorProperty(rect, _topRight);

                            rect = EditorGUILayout.GetControlRect(false,
                                EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2));
                            rect.x += EditorGUIUtility.labelWidth;
                            rect.width = (rect.width - EditorGUIUtility.labelWidth) / 2f;
                            rect.height = EditorGUIUtility.singleLineHeight * (EditorGUIUtility.wideMode ? 1 : 2);

                            TMP_EditorUtility.DrawColorProperty(rect, _bottomLeft);
                            rect.x += rect.width;
                            TMP_EditorUtility.DrawColorProperty(rect, _bottomRight);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                EditorGUI.indentLevel--;

                EditorGUILayout.Space();
            }

            // FONT SIZE
            EditorGUI.BeginChangeCheck();

            EditorGUI.BeginDisabledGroup(_autoSize.boolValue);
            EditorGUILayout.PropertyField(_fontSize, new GUIContent("Font Size"), GUILayout.MaxWidth(EditorGUIUtility.labelWidth + 50f));
            EditorGUI.EndDisabledGroup();

            if (EditorGUI.EndChangeCheck())
            {
                var fontSize = Mathf.Clamp(_fontSize.floatValue, 0, 32767);

                _fontSize.floatValue = fontSize;
                _fontSizeBase.floatValue = fontSize;
            }

            EditorGUI.indentLevel += 1;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_autoSize, new GUIContent("Auto Size"));
            if (EditorGUI.EndChangeCheck())
            {
                if (_autoSize.boolValue == false)
                    _fontSize.floatValue = _fontSizeBase.floatValue;
            }

            // AUTO SIZE OPTIONS
            if (_autoSize.boolValue)
            {
                var rect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight);

                EditorGUI.PrefixLabel(rect, new GUIContent("Auto Size Options"));

                var previousIndent = EditorGUI.indentLevel;

                EditorGUI.indentLevel = 0;

                rect.width = (rect.width - EditorGUIUtility.labelWidth) / 4f;
                rect.x += EditorGUIUtility.labelWidth;

                EditorGUIUtility.labelWidth = 24;
                EditorGUI.BeginChangeCheck();
                EditorGUI.PropertyField(rect, _minFontSize, new GUIContent("Min", "The minimum font size."));
                if (EditorGUI.EndChangeCheck())
                {
                    var minSize = _minFontSize.floatValue;

                    minSize = Mathf.Max(0, minSize);

                    _minFontSize.floatValue = Mathf.Min(minSize, _maxFontSize.floatValue);
                }
                rect.x += rect.width;

                EditorGUIUtility.labelWidth = 27;
                EditorGUI.BeginChangeCheck();
                EditorGUI.PropertyField(rect, _maxFontSize, new GUIContent("Max", "The maximum font size."));
                if (EditorGUI.EndChangeCheck())
                {
                    var maxSize = Mathf.Clamp(_maxFontSize.floatValue, 0, 32767);

                    _maxFontSize.floatValue = Mathf.Max(_minFontSize.floatValue, maxSize);
                }
                rect.x += rect.width;

                EditorGUI.indentLevel = previousIndent;
            }

            EditorGUI.indentLevel -= 1;

            //EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }
        /// <summary>
        /// Function to get the material presets and names.
        /// </summary>
        /// <returns>GUIContent item for displaying material presets</returns>
        private GUIContent[] GetMaterialPresets()
        {
            var fontAsset = _fontAsset.objectReferenceValue as TMP_FontAsset;
            if (fontAsset == null) return null;

            materialPresets = TMP_EditorUtility.FindMaterialReferences(fontAsset);
            materialPresetNames = new GUIContent[materialPresets.Length];

            _materialPresetIndexLookup.Clear();

            for (var i = 0; i < materialPresetNames.Length; i++)
            {
                materialPresetNames[i] = new GUIContent(materialPresets[i].name);

                _materialPresetIndexLookup.Add(materialPresets[i].GetInstanceID(), i);
            }

            return materialPresetNames;
        }

    }
}
