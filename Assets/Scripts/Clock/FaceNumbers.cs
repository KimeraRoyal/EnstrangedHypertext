using UnityEngine;

namespace EHT.Clock
{
    public class FaceNumbers : MonoBehaviour
    {
        private FaceNumber[] faceNumbers;
        
        [SerializeField] private bool freakingOut;

        public bool FreakingOut
        {
            get => freakingOut;
            set
            {
                if(freakingOut == value) { return; }
                freakingOut = value;
                foreach (var faceNumber in faceNumbers)
                {
                    faceNumber.FreakingOut = freakingOut;
                }
            }
        }

        private void Awake()
        {
            faceNumbers = GetComponentsInChildren<FaceNumber>();
        }

        private void Update()
        {
            foreach (var faceNumber in faceNumbers)
            {
                faceNumber.FreakingOut = freakingOut;
            }
        }
    }
}
