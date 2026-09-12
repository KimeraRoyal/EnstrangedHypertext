using System;
using System.Collections.Generic;
using UnityEngine;

namespace EHT.Timeline
{
    public class TimelineCharacters : MonoBehaviour
    {
        [SerializeField] private Character[] characters;
        private readonly Dictionary<string, int> characterAtlas = new();
        private bool atlasDirty;

        public Character[] Characters => characters;

        public Dictionary<string, int> CharacterAtlas
        {
            get
            {
                if (!atlasDirty) { ConstructAtlas(); }
                return characterAtlas;
            }
        }

        public int Count => characters.Length;

        public Character this[int i] => characters[i];

        public Character this[string i] => characters[CharacterAtlas[i]];

        public int IndexOf(Character character)
            => Array.IndexOf(characters, character);

        public int IndexOf(string i)
            => CharacterAtlas[i];

        private void ConstructAtlas()
        {
            characterAtlas.Clear();
            for (var character = 0; character < Count; character++)
            {
                characterAtlas.Add(characters[character].ID, character);
            }
            atlasDirty = true;
        }
    }
}