using FireEmblemDuplicate.Message;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public interface IBaseUnitInteraction
    {
        public void OnUnitClick(OnClickUnit message);
        public void OnUnitDrag(OnDragUnit message);
    }
}