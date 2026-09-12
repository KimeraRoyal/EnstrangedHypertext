using System;
using EHT.Timeline;
using UnityEngine;

namespace EHT
{
    [RequireComponent(typeof(LineRenderer))]
    public class BeatConnection : MonoBehaviour
    {
        private BeatConnections connections;
        
        private LineRenderer line;

        [SerializeField] private TimelineBeat a;
        [SerializeField] private TimelineBeat b;
        private bool connectionActive;
        
        public TimelineBeat A
        {
            get => a;
            set
            {
                if (a) { a.OnDependentRemoved.RemoveListener(BreakConnection); }
                a = value;
                if(!a) { return; }
                a.OnDependentRemoved.AddListener(BreakConnection);
                UpdateConnection();
            }
        }
        
        public TimelineBeat B
        {
            get => b;
            set
            {
                if (b) { b.OnDependencyRemoved.RemoveListener(BreakConnection); }
                b = value;
                if(!b) { return; }
                b.OnDependencyRemoved.AddListener(BreakConnection);
                UpdateConnection();
            }
        }
        
        private void Awake()
        {
            connections = GetComponentInParent<BeatConnections>();
            
            line = GetComponent<LineRenderer>();
        }

        public void UpdateConnection()
        {
            if(!a || !b) { return; }
            var positions = new[] { a.transform.position, b.transform.position };
            line.SetPositions(positions);
        }

        private void BreakConnection(TimelineBeat other)
        {
            if(!a || !b) { return; }
            A = null;
            B = null;
            connections.Return(this);
        }
    }
}
