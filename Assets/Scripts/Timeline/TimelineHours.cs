using UnityEngine;

namespace EHT.Timeline
{
    public class TimelineHours : MonoBehaviour
    {
        [SerializeField] private Hour[] hours;

        public int Count => hours.Length;
        public Hour this[int i] => hours[i];
    }
}