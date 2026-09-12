using UnityEngine;

namespace EHT.Timeline
{
    public class TimelineBeatSpawner : MonoBehaviour
    {
        [SerializeField] private TimelineBeat beatPrefab;
        
        [SerializeField] private float columnOffset = 1.0f, rowOffset = 1.0f;

        public int ColumnCount { get; set; }
        public int RowCount { get; set; }

        private Vector3 Offset => new Vector3((ColumnCount - 1) * -columnOffset, (RowCount - 1) * -rowOffset) / 2.0f;

        public TimelineBeat Spawn(int column, int row)
        {
            var offset = Offset + new Vector3(column * columnOffset, row * rowOffset);
            return Instantiate(beatPrefab, transform.position + offset, Quaternion.identity, transform);
        }
    }
}