using FireEmblemDuplicate.Scene.Battle.Stage.Enum;

namespace FireEmblemDuplicate.Message
{
    public struct ChangeStageInPhaseMessage
    {
        public InPhaseEnum InPhase { get; private set; }

        public ChangeStageInPhaseMessage(InPhaseEnum inPhase) 
        { 
            InPhase = inPhase;
        }
    }
}