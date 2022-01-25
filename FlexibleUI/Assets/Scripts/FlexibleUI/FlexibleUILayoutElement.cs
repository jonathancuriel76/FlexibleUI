using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [RequireComponent(typeof(LayoutElement))]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class FlexibleUILayoutElement : FlexibleUI
    {
        [SerializeField] public FlexibleUILayoutElementData layoutElementData;
        [SerializeField] public LayoutElement layoutElement;

        public void Awake()
        {
            OnSkinUI();
        }

        public override void OnSkinUI()
        {
            if (layoutElement == null)
            {
                SetupLayoutElement();
            }

            base.OnSkinUI();

            if (layoutElementData == null) return;
            layoutElement.ignoreLayout = layoutElementData.ignoreLayout;
            layoutElement.minWidth = layoutElementData.minWidth;
            layoutElement.minHeight = layoutElementData.minHeight;
            layoutElement.preferredWidth = layoutElementData.preferredWidth;
            layoutElement.preferredHeight = layoutElementData.preferredHeight;
            layoutElement.flexibleWidth = layoutElementData.flexibleWidth;
            layoutElement.flexibleHeight = layoutElementData.flexibleHeight;
            layoutElement.layoutPriority = layoutElementData.layoutPriority;

            LayoutRebuilder.MarkLayoutForRebuild(gameObject.GetComponent<RectTransform>());
        }

        private void SetupLayoutElement()
        {
            layoutElement = GetComponent<LayoutElement>();
        }

        public void OnValidate()
        {
            OnSkinUI();
        }
    }
}
