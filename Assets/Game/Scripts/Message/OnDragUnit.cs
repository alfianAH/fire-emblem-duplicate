using UnityEngine;

namespace FireEmblemDuplicate.Message
{
    public struct OnDragUnit
    {
        public Vector3 MousePosition { get; private set; }

        public OnDragUnit(Vector3 mousePosition)
        {
            MousePosition = mousePosition;
        }
    }
}