using UnityEngine;

namespace FireEmblemDuplicate.Core.PubSub
{
    public abstract class Connector<T, C> : MonoBehaviour
        where T : MonoBehaviour
        where C : MonoBehaviour
    {
        protected C controller;

        public abstract void Subscribe();

        public abstract void Unsubscribe();

        private void Awake()
        {
            controller = GetComponent<C>();
        }

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