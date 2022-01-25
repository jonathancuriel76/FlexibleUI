using TMPro;
using UnityEngine;

namespace Assets.Scripts.FlexibleUI
{
    [CreateAssetMenu(fileName = "TMPData", menuName = "Flexible UI Data/Text Mesh Pro Data")]
    public class FlexibleUITextMeshProData : ScriptableObject
    {
        [SerializeField] public TMP_FontAsset fontAsset;
        [SerializeField] public Material sharedMaterial;
        [SerializeField] public FontStyles fontStyle = FontStyles.Normal;
        [SerializeField] public float fontSize = 24.0f;
        [SerializeField] public float fontSizeBase = 24.0f;
        [SerializeField] public bool autoSize;
        [SerializeField] public float minFontSize;
        [SerializeField] public float maxFontSize;
        [SerializeField] public Color vertexColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        [SerializeField] public bool useColorGradient;
        [SerializeField] public ColorMode colorMode = ColorMode.FourCornersGradient;
        [SerializeField] public TMP_ColorGradient gradientPreset;
        [SerializeField] public Color topLeft = Color.white;
        [SerializeField] public Color topRight = Color.white;
        [SerializeField] public Color bottomLeft = Color.white;
        [SerializeField] public Color bottomRight = Color.white;

        public void SetDefaultFontAsset()
        {
            if (fontAsset != null) return;
            fontAsset = TMP_Settings.defaultFontAsset;
        }

        private void Reset()
        {
            SetDefaultFontAsset();
            OnValidate();
        }

        public void OnValidate()
        {
            var text = FindObjectsOfType<FlexibleUITextMeshPro>();
            foreach (var t in text)
            {
                t.OnSkinUI();
            }
        }
    }
}
