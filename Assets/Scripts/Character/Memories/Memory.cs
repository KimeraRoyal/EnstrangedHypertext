using System;
using UnityEngine;

namespace EHT
{
    [CreateAssetMenu(menuName = "EHT/Memory", fileName = "Memory")]
    public class Memory : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] [TextArea(3, 5)] private string description;

        public string ID => id;
        public string Description => description;

        private void Awake()
        {
            if (string.IsNullOrEmpty(id))
            {
                id = name.ToLower().Replace(' ', '-');
            }
        }

        private void OnValidate()
        {
            id = id.ToLower();
        }
    }
}
