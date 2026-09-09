using System;
using System.Collections.Generic;
using UnityEngine;

namespace EHT.Timeline.Visuals
{
    [Serializable]
    public class BeatState
    {
        [SerializeField] private CharacterState characterState;

        public CharacterState CharacterState => characterState;

        public void Reset()
        {
            characterState.Reset();
        }
        
        public void Copy(BeatState from)
        {
            characterState.Copy(from.characterState);
        }

        public void Inherit(BeatState from)
        {
            characterState.Inherit(from.characterState);
        }
    }
}