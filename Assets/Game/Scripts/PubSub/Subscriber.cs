using UnityEngine;

namespace FireEmblemDuplicate.PubSub
{
    public abstract class Subscriber<T> : MonoBehaviour
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