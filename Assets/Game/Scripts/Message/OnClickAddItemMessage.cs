using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct OnClickAddItemMessage
    {
        public BaseUnitController Unit { get; private set; }

        public OnClickAddItemMessage(BaseUnitController unit)
        {
            Unit = unit;
        }
    }
}