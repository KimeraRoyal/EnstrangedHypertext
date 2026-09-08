using EHT.Timeline.Visuals;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace EHT.Timeline
{
    public class TimelineBeat : MonoBehaviour
    {
        [SerializeField] private TimelineBeat parent;
        [SerializeField] private TimelineBeat child;
        
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
                gameObject.name = label.text;
            }
        }
        
        public int Time { get; set; }

        public BeatState State => state;

        public UnityEvent<TimelineBeat> OnBeatSelected;
        
        // TODO: Make this happen in a different class
        [SerializeField] private Image image;
        private TMP_Text label;
        
        private Button button;

        private void Awake()
        {
            label = GetComponentInChildren<TMP_Text>();
            
            button = GetComponentInChildren<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnBeatSelected?.Invoke(this);
        }

        public void Complete()
        {
            if(!child) { return; }
            child.state.Copy(state);
        }

        public void InheritFrom(TimelineBeat parent)
        {
            if(!parent || this.parent || !parent.InheritTo(this)) { return; }
            this.parent = parent;
        }

        private bool InheritTo(TimelineBeat child)
        {
            if(!child || this.child) { return false; }
            this.child = child;
            return true;
        }
    }
}