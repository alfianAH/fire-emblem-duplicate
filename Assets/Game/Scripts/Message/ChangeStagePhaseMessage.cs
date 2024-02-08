using FireEmblemDuplicate.Scene.Battle.Stage.Enum;

namespace FireEmblemDuplicate.Message
{
    public struct ChangeStagePhaseMessage
    {
        public StagePhase NewPhase { get; private set; }

        public ChangeStagePhaseMessage(StagePhase newPhase)
        {
            NewPhase = newPhase;
        }
    }
}