using DG.Tweening;
using UnityEngine;

namespace EHT.Clock
{
    [RequireComponent(typeof(Clock))]
    public class ClockManipulator : MonoBehaviour
    {
        private Clock clock;

        [SerializeField] private float moveForwardDuration = 1.0f;
        [SerializeField] private Ease moveForwardEase = Ease.OutBounce;

        private Tween tween;

        private void Awake()
        {
            clock = GetComponent<Clock>();
        }

        public void SetHour(int hour)
        {
            var ticks = Clock.HourTicks * hour;
            Manipulate(ticks, moveForwardDuration, moveForwardEase);
        }

        private void Manipulate(int targetTicks, float duration, Ease ease)
        {
            if(tween is { active: true }) { tween.Kill(); }

            tween = DOTween.To(() => clock.Ticks, value => clock.Ticks = value, targetTicks, duration).SetEase(ease);
        }
    }
}