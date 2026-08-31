using EHT.Clock;
using UnityEngine;

namespace EHT.Timeline.Clock
{
    public class TimelineClockSetter : MonoBehaviour
    {
        private Timeline timeline;
        private ClockManipulator clock;

        private void Awake()
        {
            timeline = FindAnyObjectByType<Timeline>();
            clock = GetComponent<ClockManipulator>();
            
            timeline.OnHourChanged.AddListener(clock.SetHour);
        }
    }
}
