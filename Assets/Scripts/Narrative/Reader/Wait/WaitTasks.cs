using System.Collections;
using UnityEngine;

namespace EHT.Narrative.Reader.Wait
{
    public class WaitForSecondsTask : ReaderTask
    {
        private MonoBehaviour coroutineOwner;
        private float duration;

        public WaitForSecondsTask(MonoBehaviour coroutineOwner, float duration)
        {
            this.coroutineOwner = coroutineOwner;
            this.duration = duration;
        }

        protected override void Execute()
        {
            coroutineOwner.StartCoroutine(WaitForSeconds());
        }

        private IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(duration);
            Completed = true;
        }
    }
    public class WaitForInputTask : ReaderTask
    {
        private InputIndicator indicator;

        public WaitForInputTask(InputIndicator arrow)
        {
            this.indicator = arrow;
        }

        protected override void Execute()
        {
            indicator.Activate();
            indicator.StartCoroutine(WaitForInput());
        }

        private IEnumerator WaitForInput()
        {
            yield return new WaitUntil(() => !indicator.Active);
            Completed = true;
        }
    }
}