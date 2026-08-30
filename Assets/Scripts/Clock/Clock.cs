using UnityEngine;

namespace EHT.Clock
{
    public class Clock : MonoBehaviour
    {
        public const int MinuteTicks = 60;
        public const int HourTicks = MinuteTicks * 60;
        public const int HalfDayTicks = HourTicks * 12;
        
        private Hand[] hands;
        
        [SerializeField] private int ticks;

        public int Ticks
        {
            get => ticks;
            set
            {
                ticks = value;
                MoveHands();
            }
        }
        
        private void Awake()
        {
            hands = GetComponentsInChildren<Hand>();
        }

        private void Start()
        {
            MoveHands();
        }

        private void MoveHands()
        {
            if (hands == null) { return; }
            foreach (var hand in hands)
            {
                hand.Ticks = ticks;
            }
        }

        private void OnValidate()
        {
            MoveHands();
        }
    }
}
