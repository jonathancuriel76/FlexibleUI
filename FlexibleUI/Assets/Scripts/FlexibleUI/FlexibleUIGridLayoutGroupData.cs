using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FlexibleUI
{
    [CreateAssetMenu(fileName = "GridLayoutGroupData", menuName = "Flexible UI Data/Grid Layout Group Data")]
    public class FlexibleUIGridLayoutGroupData : ScriptableObject
    {
        [SerializeField] public RectOffset padding = new RectOffset();
        [SerializeField] public Vector2 cellSize = new Vector2(100, 100);
        [SerializeField] public Vector2 spacing = Vector2.zero;
        [SerializeField] public GridLayoutGroup.Corner startCorner = GridLayoutGroup.Corner.UpperLeft;
        [SerializeField] public GridLayoutGroup.Axis startAxis = GridLayoutGroup.Axis.Horizontal;
        [SerializeField] public TextAnchor childAlignment = TextAnchor.UpperLeft;
        [SerializeField] public GridLayoutGroup.Constraint constraint = GridLayoutGroup.Constraint.Flexible;
        [SerializeField] public int constraintCount = 2;

#if UNITY_EDITOR
        public void OnValidate()
        {
            var gridLayoutGroups = FindObjectsOfType<FlexibleUIGridLayoutGroup>();
            foreach (var t in gridLayoutGroups)
            {
                t.OnSkinUI();
            }
        }
#endif
    }
}
