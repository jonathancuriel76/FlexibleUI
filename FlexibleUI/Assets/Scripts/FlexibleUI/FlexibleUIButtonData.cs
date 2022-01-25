using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [CreateAssetMenu(fileName = "ButtonData", menuName = "Flexible UI Data/Button Data")]
    public class FlexibleUIButtonData : ScriptableObject
    {
        [SerializeField] public Selectable.Transition transition = Selectable.Transition.ColorTint;
        [SerializeField] public ColorBlock colors = ColorBlock.defaultColorBlock;
        [SerializeField] public SpriteState spriteState = new SpriteState();
        [SerializeField] public AnimationTriggers animationTriggers = new AnimationTriggers();
        [SerializeField] public Navigation navigationMode = Navigation.defaultNavigation;

#if UNITY_EDITOR
        public void OnValidate()
        {
            var buttons = FindObjectsOfType<FlexibleUIButton>();
            foreach (var t in buttons)
            {
                t.OnSkinUI();
            }
        }
#endif
    }
}
