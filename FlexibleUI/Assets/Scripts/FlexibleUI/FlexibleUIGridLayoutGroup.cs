using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class FlexibleUIGridLayoutGroup : FlexibleUI
    {
        [SerializeField] public FlexibleUIGridLayoutGroupData gridLayoutGroupData;
        [SerializeField] public GridLayoutGroup gridLayoutGroup;

        public void Awake()
        {
            OnSkinUI();
        }

        public override void OnSkinUI()
        {
            if (gridLayoutGroup == null)
            {
                SetupGridLayoutGroup();
            }

            base.OnSkinUI();

            if (gridLayoutGroupData == null) return;
            gridLayoutGroup.padding = gridLayoutGroupData.padding;
            gridLayoutGroup.cellSize = gridLayoutGroupData.cellSize;
            gridLayoutGroup.spacing = gridLayoutGroupData.spacing;
            gridLayoutGroup.startCorner = gridLayoutGroupData.startCorner;
            gridLayoutGroup.startAxis = gridLayoutGroupData.startAxis;
            gridLayoutGroup.childAlignment = gridLayoutGroupData.childAlignment;
            gridLayoutGroup.constraint = gridLayoutGroupData.constraint;
            gridLayoutGroup.constraintCount = gridLayoutGroupData.constraintCount;

            LayoutRebuilder.MarkLayoutForRebuild(gameObject.GetComponent<RectTransform>());
        }

        private void SetupGridLayoutGroup()
        {
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }

        public void OnValidate()
        {
            OnSkinUI();
        }
    }
}
