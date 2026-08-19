using UnityEngine;

namespace EHT.Narrative.Reader
{
    [CreateAssetMenu(fileName = "Writing Profile", menuName = "EHT/Writing Profile", order = 0)]
    public class WritingProfile : ScriptableObject
    {
        [SerializeField] [Min(0.0f)] private float letterInterval;
        [SerializeField] private bool useLetterInterval;
        [SerializeField] [Min(0.0f)] private float wordInterval;
        [SerializeField] private bool useWordInterval;
        [SerializeField] [Min(0.0f)] private float punctuationInterval;
        [SerializeField] private bool usePunctuationInterval;

        public float LetterInterval => letterInterval;
        public bool UseLetterInterval => useLetterInterval;

        public float WordInterval => wordInterval;
        public bool UseWordInterval => useWordInterval;

        public float PunctuationInterval => punctuationInterval;
        public bool UsePunctuationInterval => usePunctuationInterval;

        public bool UseIntervals => (UseLetterInterval || UseWordInterval || UsePunctuationInterval) && (LetterInterval + WordInterval + PunctuationInterval > 0.001f);
    }
}