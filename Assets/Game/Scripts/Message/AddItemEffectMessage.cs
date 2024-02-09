using FireEmblemDuplicate.Scene.Battle.Item.Enum;
using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct AddItemEffectMessage
    {
        public BaseUnitController TargetUnit { get; private set; }
        public ItemType Type { get; private set; }
        public float Amount { get; private set; }

        public AddItemEffectMessage(BaseUnitController targetUnit, ItemType type, float amount)
        {
            TargetUnit = targetUnit;
            Type = type;
            Amount = amount;
        }
    }
}