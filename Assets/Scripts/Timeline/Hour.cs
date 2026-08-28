using UnityEngine;

namespace EHT.Timeline
{
    [CreateAssetMenu(menuName = "EHT/Beat", fileName = "Beat")]
    public class Hour : ScriptableObject
    {
        [SerializeField] private TextAsset inkScript;

        public TextAsset InkScript => inkScript;
    }
}
