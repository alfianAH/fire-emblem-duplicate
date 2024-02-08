using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct UnitDeadMessage
    {
        public BaseUnitController Unit { get; private set; }

        public UnitDeadMessage(BaseUnitController unit)
        {
            Unit = unit;
        }
    }
}