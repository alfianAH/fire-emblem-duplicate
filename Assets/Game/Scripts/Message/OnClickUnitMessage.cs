using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct OnClickUnitMessage
    {
        public BaseUnitController ClickedUnit { get; private set; }

        public OnClickUnitMessage(BaseUnitController clickedUnit)
        {
            ClickedUnit = clickedUnit;
        }
    }
}
