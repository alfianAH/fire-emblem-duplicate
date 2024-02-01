using UnityEngine;

namespace FireEmblemDuplicate.Core.PubSub
{
    public abstract class Connector<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        public abstract void Subscribe();

        public abstract void Unsubscribe();

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }
    }
}