using FireEmblemDuplicate.Message;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public interface IBaseUnitInteraction
    {
        public void OnUnitClick(OnClickUnitMessage message);
        public void OnStartDragUnit(OnStartDragUnitMessage message);
        public void OnEndDragUnit(OnEndDragUnitMessage message);
    }
}