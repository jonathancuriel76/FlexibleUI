using TMPro;
using UnityEngine;

namespace Assets.Scripts.FlexibleUI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class FlexibleUITextMeshPro : FlexibleUI
    {
        [SerializeField] public FlexibleUITextMeshProData tmpData;
        [SerializeField] public TextMeshProUGUI text;

        public void Awake()
        {
            OnSkinUI();
        }

        public override void OnSkinUI()
        {
            if (text == null)
            {
                SetupText();
            }

            base.OnSkinUI();

            if (tmpData == null) return;
            // if null, set fontAsset to default TMPro fontAsset
            if (tmpData.fontAsset == null)
            {
                tmpData.SetDefaultFontAsset();
            }
            // update font asset
            text.font = tmpData.fontAsset;
            // updated material
            text.fontSharedMaterial = tmpData.sharedMaterial;
            // update font style
            text.fontStyle = tmpData.fontStyle;
            // update font color
            text.color = tmpData.vertexColor;
            // auto size
            text.enableAutoSizing = tmpData.autoSize;
            // min and max size
            if (text.enableAutoSizing)
            {
                text.fontSizeMin = tmpData.minFontSize;
                text.fontSizeMax = tmpData.maxFontSize;
            }
            else
            {
                // update font size
                text.fontSize = tmpData.fontSize;
            }
            // enable gradient color
            text.enableVertexGradient = tmpData.useColorGradient;
            // if using gradient
            if (!text.enableVertexGradient) return;
            // update gradient preset if not null
            if (tmpData != null)
                text.colorGradientPreset = tmpData.gradientPreset;
            // update colorGradient
            text.colorGradient = new VertexGradient(tmpData.topLeft, tmpData.topRight,
                tmpData.bottomLeft, tmpData.bottomRight);
        }

        /// <summary>
        /// Function to get TextMeshProUGUI component, required if script is attached.
        /// </summary>
        private void SetupText()
        {
            text = GetComponent<TextMeshProUGUI>();
        }
    }
}
