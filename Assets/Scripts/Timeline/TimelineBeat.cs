using EHT.Timeline.Visuals;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EHT.Timeline
{
    public class TimelineBeat : MonoBehaviour
    {
        private Character character;
        private Hour hour;
        
        [SerializeField] private BeatState state;

        public Character Character
        {
            get => character;
            set
            {
                character = value;
                image.color = character.Color;
            }
        }

        public Hour Hour
        {
            get => hour;
            set
            {
                hour = value;
                label.text = $"{character?.name} {hour.name}";
            }
        }
        
        public int Time { get; set; }

        public BeatState State => state;

        public UnityEvent<TimelineBeat> OnBeatSelected;
        
        private Image image;
        private TMP_Text label;
        
        private Button button;

        private void Awake()
        {
            image = GetComponent<Image>();
            label = GetComponentInChildren<TMP_Text>();
            
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnBeatSelected?.Invoke(this);
        }
    }
}