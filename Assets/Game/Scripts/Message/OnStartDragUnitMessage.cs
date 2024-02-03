using UnityEngine;

namespace FireEmblemDuplicate.Message
{
    public struct OnStartDragUnitMessage
    {
        public GameObject SelectedObject { get; private set; }
        public float PositionValue { get; private set; }

        public OnStartDragUnitMessage(GameObject selectedObject, float positionValue)
        {
            SelectedObject = selectedObject;
            PositionValue = positionValue;
        }
    }
}