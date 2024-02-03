using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct ChangeCurrentUnitOnClickMessage
    {
        public BaseUnitController UnitController { get; private set; }

        public ChangeCurrentUnitOnClickMessage(BaseUnitController unitController)
        {
            UnitController = unitController;
        }
    }
}