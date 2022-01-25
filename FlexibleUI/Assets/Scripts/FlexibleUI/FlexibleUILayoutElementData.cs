using UnityEngine;

namespace Assets.Scripts.FlexibleUI
{
    [CreateAssetMenu(fileName = "LayoutElementData", menuName = "Flexible UI Data/Layout Element Data")]
    public class FlexibleUILayoutElementData : ScriptableObject
    {
        [SerializeField] public bool ignoreLayout = false;
        [SerializeField] public float minWidth = -1f;
        [SerializeField] public float minHeight = -1f;
        [SerializeField] public float preferredWidth = -1f;
        [SerializeField] public float preferredHeight = -1f;
        [SerializeField] public float flexibleWidth = -1f;
        [SerializeField] public float flexibleHeight = -1f;
        [SerializeField] public int layoutPriority = 1;

#if UNITY_EDITOR
        public void OnValidate()
        {
            var layoutGroups = FindObjectsOfType<FlexibleUILayoutElement>();
            foreach (var t in layoutGroups)
            {
                t.OnSkinUI();
            }
        }
#endif
    }
}
