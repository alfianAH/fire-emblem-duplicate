using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct SetCurrentUnitOnClickMessage
    {
        public BaseUnitController UnitController { get; private set; }

        public SetCurrentUnitOnClickMessage(BaseUnitController unitController)
        {
            UnitController = unitController;
        }
    }
}
