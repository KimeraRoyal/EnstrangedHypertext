using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EHT
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FaceNumber : MonoBehaviour
    {
        private SpriteRenderer sprite;

        [SerializeField] [Range(1, 12)] private int value;
        
        [SerializeField] private Sprite[] numbers;

        [SerializeField] private float minRandomizeInterval = 1.0f, maxRandomizeInterval = 1.0f;
        [SerializeField] private float minRandomizeDuration = 0.1f, maxRandomizeDuration = 0.1f;
        private float timer, timerTarget;
        private bool randomizing;
        [SerializeField] private bool freakingOut;

        [SerializeField] private int randomizeChainChance = 5;
        [SerializeField] private int randomizeChainIncrement = 1;
        private int currentChainChance;

        public bool FreakingOut
        {
            get => freakingOut;
            set
            {
                if(freakingOut == value) { return; }
                freakingOut = value;
                
                if(!freakingOut) { return; }
                RandomizeChain();
                UpdateTimerTargets();
            }
        }

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            UpdateTimerTargets();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if(timer < timerTarget) { return; }
            timer -= timerTarget;

            RandomizeChain();
            UpdateTimerTargets();
        }

        private void RandomizeChain()
        {
            if (freakingOut)
            {
                randomizing = true;
                return;
            }
            
            if (randomizing && Random.Range(0, currentChainChance) < 1)
            {
                currentChainChance += randomizeChainIncrement;
            }
            else
            {
                randomizing = !randomizing;
                currentChainChance = randomizeChainChance;
            }
        }

        private void UpdateTimerTargets()
        {
            float min, max;
            if (randomizing)
            {
                sprite.sprite = numbers[Random.Range(0, numbers.Length)];
                
                min = minRandomizeDuration;
                max = maxRandomizeDuration;
            }
            else
            {
                sprite.sprite = numbers[value - 1];
                
                min = minRandomizeInterval;
                max = maxRandomizeInterval;
            }
            timerTarget = Random.Range(min, max);
        }
    }
}
