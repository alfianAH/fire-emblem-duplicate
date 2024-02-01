using UnityEngine;

namespace FireEmblemDuplicate.Message
{
    public struct OnStartDragUnit
    {
        public GameObject SelectedObject { get; private set; }
        public float PositionValue { get; private set; }

        public OnStartDragUnit(GameObject selectedObject, float positionValue)
        {
            SelectedObject = selectedObject;
            PositionValue = positionValue;
        }
    }
}