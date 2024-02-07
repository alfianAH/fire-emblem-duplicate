using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct OnBattleFinishMessage
    {
        public BaseUnitController Attacker { get; private set; }

        public OnBattleFinishMessage(BaseUnitController attacker)
        {
            Attacker = attacker;
        }
    }
}
