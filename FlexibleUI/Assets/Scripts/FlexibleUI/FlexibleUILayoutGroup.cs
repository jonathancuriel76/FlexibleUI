using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [RequireComponent(typeof(HorizontalOrVerticalLayoutGroup))]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class FlexibleUILayoutGroup : FlexibleUI
    {
        [SerializeField] public FlexibleUILayoutGroupData layoutGroupData;
        [SerializeField] public HorizontalOrVerticalLayoutGroup layoutGroup;

        public void Awake()
        {
            OnSkinUI();
        }

        public override void OnSkinUI()
        {
            if (layoutGroup == null)
            {
                SetupLayoutGroup();
            }

            base.OnSkinUI();

            if (layoutGroupData == null) return;
            layoutGroup.padding = layoutGroupData.padding;
            layoutGroup.spacing = layoutGroupData.spacing;
            layoutGroup.childAlignment = layoutGroupData.childAlignment;
            layoutGroup.childControlWidth = layoutGroupData.childControlWidth;
            layoutGroup.childControlHeight = layoutGroupData.childControlHeight;
            layoutGroup.childForceExpandWidth = layoutGroupData.childForceExpandWidth;
            layoutGroup.childForceExpandHeight = layoutGroupData.childForceExpandHeight;

            LayoutRebuilder.MarkLayoutForRebuild(gameObject.GetComponent<RectTransform>());
        }

        private void SetupLayoutGroup()
        {
            layoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
        }

        public void OnValidate()
        {
            OnSkinUI();
        }
    }
}
