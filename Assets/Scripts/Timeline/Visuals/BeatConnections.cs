using EHT.Timeline;
using UnityEngine;
using UnityEngine.Pool;

namespace EHT
{
    public class BeatConnections : MonoBehaviour
    {
        [SerializeField] private BeatConnection connectionPrefab;
        private ObjectPool<BeatConnection> connectionPool;

        public BeatConnection Connect(TimelineBeat parent, TimelineBeat child)
        {
            var connection = connectionPool.Get();
            connection.A = parent;
            connection.B = child;
            return connection;
        }

        public void Return(BeatConnection connection)
        {
            connectionPool.Release(connection);
        }
        
        private void Awake()
        {
            connectionPool = new ObjectPool<BeatConnection>(Create, OnTaken, OnReturned, Destroy);
        }

        private BeatConnection Create()
        {
            var connection = Instantiate(connectionPrefab, transform);
            return connection;
        }

        private void Destroy(BeatConnection connection)
        {
            Destroy(connection.gameObject);
        }

        private void OnTaken(BeatConnection connection)
        {
            connection.gameObject.SetActive(true);
        }

        private void OnReturned(BeatConnection connection)
        {
            connection.gameObject.SetActive(false);
        }
    }
}
