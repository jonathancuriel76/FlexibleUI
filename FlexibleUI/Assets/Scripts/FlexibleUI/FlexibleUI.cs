using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [ExecuteInEditMode]
    public class FlexibleUI : MonoBehaviour
    {
        // variables to work with commented editor functions in FlexibleUIEditor.cs 
        //[SerializeField] public string[] dataFilePaths;
        //[SerializeField] public string[] dataOptions;

        public virtual void OnSkinUI()
        {

        }

        // Helper function for UI components that have Transition set to a value other than Color Tint. 
        // Used to keep the Target Graphic color white so that Color Tint doesn't affect it.
        protected static void StartColorTween(Graphic targetGraphic, ColorBlock colorBlock, Color targetColor, bool instant)
        {
            if (targetGraphic == null)
                return;

            targetGraphic.CrossFadeColor(targetColor, instant ? 0f : colorBlock.fadeDuration, true, true);
        }
    }
}
