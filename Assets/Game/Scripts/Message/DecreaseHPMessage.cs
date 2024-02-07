using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct DecreaseHPMessage
    {
        public BaseUnitController Defender { get; private set; }
        public float Amount { get; private set; }

        public DecreaseHPMessage(BaseUnitController defender, float amount)
        {
            Defender = defender;
            Amount = amount;
        }
    }
}