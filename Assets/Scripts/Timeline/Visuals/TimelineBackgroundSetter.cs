using System;
using DG.Tweening;
using UnityEngine;

namespace EHT.Timeline.Visuals
{
    public class TimelineBackgroundSetter : MonoBehaviour
    {
        private Timeline timeline;
        private Camera camera;

        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private float fadeDuration = 1.0f;

        private Tween fadeTween;
        
        private void Awake()
        {
            timeline = FindAnyObjectByType<Timeline>();
            camera = GetComponent<Camera>();
            
            timeline.Beats.OnBeatSelected.AddListener(SelectBeat);
        }

        private void Start()
        {
            camera.backgroundColor = defaultColor;
        }

        private void SelectBeat(TimelineBeat beat)
        {
            if(fadeTween is { active: true }) { fadeTween.Kill(); }

            fadeTween = camera.DOColor(beat ? beat.Character.BackgroundColor : defaultColor, fadeDuration);
        }
    }
}
