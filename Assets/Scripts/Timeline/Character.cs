using UnityEngine;

namespace EHT.Timeline
{
    [CreateAssetMenu(menuName = "EHT/Character", fileName = "Character")]
    public class Character : ScriptableObject
    {
        [SerializeField] private Color color = Color.white;

        public Color Color => color;
    }
}
