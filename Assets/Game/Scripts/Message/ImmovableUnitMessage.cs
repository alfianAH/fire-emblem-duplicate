using FireEmblemDuplicate.Scene.Battle.Unit.Enum;

namespace FireEmblemDuplicate.Message
{
    public struct ImmovableUnitMessage
    {
        public UnitSide UnitSide { get; private set; }

        public ImmovableUnitMessage(UnitSide unitSide)
        {
            UnitSide = unitSide;
        }
    }
}