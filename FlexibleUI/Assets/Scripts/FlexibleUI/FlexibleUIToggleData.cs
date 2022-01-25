using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [CreateAssetMenu(fileName = "ToggleData", menuName = "Flexible UI Data/Toggle Data")]
    public class FlexibleUIToggleData : ScriptableObject
    {
        [SerializeField] public Selectable.Transition transition = Selectable.Transition.ColorTint;
        [SerializeField] public ColorBlock colors = ColorBlock.defaultColorBlock;
        [SerializeField] public SpriteState spriteState;
        [SerializeField] public AnimationTriggers animationTriggers = new AnimationTriggers();
        [SerializeField] public Navigation navigationMode = Navigation.defaultNavigation;
        [SerializeField] public Toggle.ToggleTransition toggleTransition = Toggle.ToggleTransition.Fade;

#if UNITY_EDITOR
        public void OnValidate()
        {
            var toggles = FindObjectsOfType<FlexibleUIToggle>();
            foreach (var t in toggles)
            {
                t.OnSkinUI();
            }
        }
#endif
    }
}
