using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [RequireComponent(typeof(Image))]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class FlexibleUIImage : FlexibleUI
    {
        [SerializeField] public FlexibleUIImageData imageData;
        [SerializeField] public Image image;

        public void Awake()
        {
            OnSkinUI();
        }

        public override void OnSkinUI()
        {
            if (image == null)
            {
                SetupImage();
            }

            base.OnSkinUI();

            if (imageData == null) return;
            image.sprite = imageData.imageSprite ? imageData.imageSprite : null;
            image.color = imageData.imageColor;
            image.material = imageData.imageMaterial ? imageData.imageMaterial : null;
            image.raycastTarget = imageData.imageRaycastTarget;
            image.type = imageData.imageType;
            image.preserveAspect = imageData.imagePreserveAspect;
            image.fillCenter = imageData.imageFillCenter;
            image.fillMethod = imageData.imageFillMethod;
            switch (imageData.imageFillMethod)
            {
                case Image.FillMethod.Horizontal:
                    image.fillOrigin = (int)imageData.imageOriginHorizontal;
                    break;
                case Image.FillMethod.Vertical:
                    image.fillOrigin = (int)imageData.imageOriginVertical;
                    break;
                case Image.FillMethod.Radial90:
                    image.fillOrigin = (int)imageData.imageOrigin90;
                    break;
                case Image.FillMethod.Radial180:
                    image.fillOrigin = (int)imageData.imageOrigin180;
                    break;
                case Image.FillMethod.Radial360:
                    image.fillOrigin = (int)imageData.imageOrigin360;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            image.fillClockwise = imageData.imageClockwise;
        }

        private void SetupImage()
        {
            image = GetComponent<Image>();
        }
    }
}
