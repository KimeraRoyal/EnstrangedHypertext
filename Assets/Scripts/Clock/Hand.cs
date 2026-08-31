using UnityEngine;

namespace EHT.Clock
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private float ticks;
        [SerializeField] [Min(1)] private float tickScale = 1;
        [SerializeField] [Min(1)] private int period = 60;
        
        public float Ticks
        {
            get => ticks;
            set
            {
                ticks = (value / tickScale) % period;
                SetRotation();
            }
        }

        private void SetRotation()
        {
            var angle = ticks / period * 360.0f;
            var angles = transform.localEulerAngles;
            angles.z = -angle;
            transform.localEulerAngles = angles;
        }
    }
}
