using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EHT.Timeline.Visuals
{
    [RequireComponent(typeof(TimelineBeat))]
    public class BeatConnector : MonoBehaviour
    {
        private BeatConnections connections;

        private TimelineBeat beat;
        
        private void Awake()
        {
            connections = FindAnyObjectByType<BeatConnections>();
            
            beat = GetComponent<TimelineBeat>();
            beat.OnDependentAdded.AddListener(Connect);
        }

        private void Connect(TimelineBeat child)
        {
            connections.Connect(beat, child);
        }
    }
}