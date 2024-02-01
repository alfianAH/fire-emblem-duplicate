using UnityEngine;

namespace FireEmblemDuplicate.Message
{
    public struct OnEndDragUnit
    {
        public GameObject SelectedObject { get; private set; }

        public OnEndDragUnit(GameObject selectedObject)
        {
            SelectedObject = selectedObject;
        }
    }
}