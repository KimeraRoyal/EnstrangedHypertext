using System;
using System.Collections.Generic;
using UnityEngine;

namespace EHT.Timeline.Visuals
{
    [Serializable]
    public class BeatState
    {
        [SerializeField] private BeatState parent;
        [SerializeField] private List<BeatState> linked = new();
        
        [SerializeField] private CharacterState characterState;

        public BeatState Parent => parent;
        public List<BeatState> Linked => linked;

        public CharacterState CharacterState => characterState;
    }
}