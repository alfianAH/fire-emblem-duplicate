using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct OnBattleBeginMessage
    {
        public BaseUnitController Ally { get; private set; }
        public BaseUnitController Enemy { get; private set; }

        public OnBattleBeginMessage(BaseUnitController ally, BaseUnitController enemy)
        {
            Ally = ally;
            Enemy = enemy;
        }
    }
}
