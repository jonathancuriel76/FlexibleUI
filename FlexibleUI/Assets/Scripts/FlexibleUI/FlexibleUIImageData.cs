using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [CreateAssetMenu(fileName = "ImageData", menuName = "Flexible UI Data/Image Data")]
    public class FlexibleUIImageData : ScriptableObject
    {
        [SerializeField] public Sprite imageSprite;
        [SerializeField] public Color imageColor = Color.white;
        [SerializeField] public Material imageMaterial;
        [SerializeField] public bool imageRaycastTarget = true;
        [SerializeField] public Image.Type imageType = Image.Type.Simple;
        [SerializeField] public Image.FillMethod imageFillMethod = Image.FillMethod.Horizontal;
        [SerializeField] public Image.OriginHorizontal imageOriginHorizontal = Image.OriginHorizontal.Left;
        [SerializeField] public Image.OriginVertical imageOriginVertical = Image.OriginVertical.Bottom;
        [SerializeField] public Image.Origin90 imageOrigin90 = Image.Origin90.BottomLeft;
        [SerializeField] public Image.Origin180 imageOrigin180 = Image.Origin180.Bottom;
        [SerializeField] public Image.Origin360 imageOrigin360 = Image.Origin360.Bottom;
        [SerializeField] public bool imageClockwise = true;
        [SerializeField] public bool imagePreserveAspect;
        [SerializeField] public bool imageFillCenter = true;

        public void OnValidate()
        {
            var images = FindObjectsOfType<FlexibleUIImage>();
            foreach (var t in images)
            {
                t.OnSkinUI();
            }
        }
    }
}
