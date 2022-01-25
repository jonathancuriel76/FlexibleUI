using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [RequireComponent(typeof(Button))]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class FlexibleUIButton : FlexibleUI
    {
        [SerializeField] public FlexibleUIButtonData buttonData;
        [SerializeField] public Button button;
        [SerializeField] public Graphic targetGraphic;

        public void Awake()
        {
            OnSkinUI();
        }

        public override void OnSkinUI()
        {
            if (button == null || targetGraphic == null)
            {
                SetupButton();
            }

            base.OnSkinUI();

            if (buttonData != null)
            {
                button.transition = buttonData.transition;
                button.colors = buttonData.colors;
                button.spriteState = buttonData.spriteState;
                button.animationTriggers = buttonData.animationTriggers;
                button.navigation = buttonData.navigationMode;
            }

            // if the transition is not Color Tint, use white to tint
            if (button.transition != Selectable.Transition.ColorTint && targetGraphic != null)
            {
                StartColorTween(targetGraphic, button.colors, Color.white, true);
            }
        }

        private void SetupButton()
        {
            button = GetComponent<Button>();
            if (button != null)
            {
                targetGraphic = button.targetGraphic;
            }
        }
    }
}
