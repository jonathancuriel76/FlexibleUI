using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [CreateAssetMenu(fileName = "SliderData", menuName = "Flexible UI Data/Slider Data")]
    public class FlexibleUISliderData : ScriptableObject
    {
        [SerializeField] public Selectable.Transition transition = Selectable.Transition.ColorTint;
        [SerializeField] public ColorBlock colors = ColorBlock.defaultColorBlock;
        [SerializeField] public SpriteState spriteState = new SpriteState();
        [SerializeField] public AnimationTriggers animationTriggers = new AnimationTriggers();
        [SerializeField] public Navigation navigationMode = Navigation.defaultNavigation;
        [SerializeField] public Slider.Direction direction = Slider.Direction.LeftToRight;
        [SerializeField] public float minValue = 0.0f;
        [SerializeField] public float maxValue = 1.0f;
        [SerializeField] public bool wholeNumbers = false;

#if UNITY_EDITOR
        public void OnValidate()
        {
            var sliders = FindObjectsOfType<FlexibleUISlider>();
            foreach (var t in sliders)
            {
                t.OnSkinUI();
            }
        }
#endif
    }
}
