using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [RequireComponent(typeof(Toggle))]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class FlexibleUIToggle : FlexibleUI
    {
        [SerializeField] public FlexibleUIToggleData toggleData;
        [SerializeField] public Toggle toggle;
        [SerializeField] public Graphic targetGraphic;

        public void Awake()
        {
            OnSkinUI();
        }

        public override void OnSkinUI()
        {
            if (toggle == null)
            {
                SetupToggle();
            }

            base.OnSkinUI();

            if (toggleData != null)
            {
                //_Toggle.interactable = _ToggleData._Interactable;
                toggle.transition = toggleData.transition;
                toggle.colors = toggleData.colors;
                toggle.spriteState = toggleData.spriteState;
                toggle.animationTriggers = toggleData.animationTriggers;
                toggle.navigation = toggleData.navigationMode;
                toggle.toggleTransition = toggleData.toggleTransition;
            }

            // if the transition is not Color Tint, uses white to tint
            if (toggle.transition != Selectable.Transition.ColorTint && targetGraphic != null)
            {
                StartColorTween(targetGraphic, toggle.colors, Color.white, true);
            }
        }

        private void SetupToggle()
        {
            toggle = GetComponent<Toggle>();
            if (toggle != null)
            {
                targetGraphic = toggle.targetGraphic;
            }
        }
    }
}
