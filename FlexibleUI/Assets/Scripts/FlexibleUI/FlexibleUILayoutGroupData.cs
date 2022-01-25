using UnityEngine;

namespace Assets.Scripts.FlexibleUI
{
    [CreateAssetMenu(fileName = "LayoutGroupData", menuName = "Flexible UI Data/Layout Group Data")]
    public class FlexibleUILayoutGroupData : ScriptableObject
    {
        [SerializeField] public RectOffset padding = new RectOffset();
        [SerializeField] public float spacing = 0f;
        [SerializeField] public TextAnchor childAlignment = TextAnchor.UpperLeft;
        [SerializeField] public bool childControlWidth = true;
        [SerializeField] public bool childControlHeight = true;
        [SerializeField] public bool childForceExpandWidth = false;
        [SerializeField] public bool childForceExpandHeight = false;

#if UNITY_EDITOR
        public void OnValidate()
        {
            var layoutGroups = FindObjectsOfType<FlexibleUILayoutGroup>();
            foreach (var t in layoutGroups)
            {
                t.OnSkinUI();
            }
        }
#endif
    }
}
