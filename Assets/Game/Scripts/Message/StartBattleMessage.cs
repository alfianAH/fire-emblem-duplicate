using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct StartBattleMessage
    {
        public BaseUnitController Attacker { get; private set; }
        public BaseUnitController Defender { get; private set; }

        public StartBattleMessage(BaseUnitController attacker, BaseUnitController defender)
        {
            Attacker = attacker;
            Defender = defender;
        }
    }
}
