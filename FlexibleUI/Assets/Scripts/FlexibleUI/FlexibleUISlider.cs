using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [RequireComponent(typeof(Slider))]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class FlexibleUISlider : FlexibleUI
    {
        [SerializeField] public FlexibleUISliderData sliderData;
        [SerializeField] public Slider slider;
        [SerializeField] public Graphic targetGraphic;

        public void Awake()
        {
            OnSkinUI();
        }

        public override void OnSkinUI()
        {
            if (slider == null)
            {
                SetupSlider();
            }

            SetupSlider();

            base.OnSkinUI();

            if (sliderData != null)
            {
                slider.transition = sliderData.transition;
                slider.colors = sliderData.colors;
                slider.spriteState = sliderData.spriteState;
                slider.animationTriggers = sliderData.animationTriggers;
                slider.navigation = sliderData.navigationMode;
                slider.direction = sliderData.direction;
                slider.minValue = sliderData.minValue;
                slider.maxValue = sliderData.maxValue;
                slider.wholeNumbers = sliderData.wholeNumbers;
            }

            // if the transition is not Color Tint, uses white to tint
            if (slider.transition != Selectable.Transition.ColorTint && targetGraphic != null)
            {
                StartColorTween(targetGraphic, slider.colors, Color.white, true);
            }
        }

        private void SetupSlider()
        {
            slider = GetComponent<Slider>();
            if (slider != null)
            {
                targetGraphic = slider.targetGraphic;
            }
        }
    }
}
