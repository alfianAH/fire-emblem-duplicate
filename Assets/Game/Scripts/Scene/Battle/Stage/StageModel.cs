using FireEmblemDuplicate.Scene.Battle.Stage.Enum;
using FireEmblemDuplicate.Scene.Battle.Unit;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Stage
{
    public class StageModel : MonoBehaviour
    {
        [SerializeField] private StagePhase _stagePhase;
        [SerializeField] private InPhaseEnum _inPhaseEnum;

        public int TurnNumber { get; private set; } = 1;
        public StageData Data { get; private set; }
        public BaseUnitController CurrentUnitOnClick { get; private set; }
        public StagePhase Phase => _stagePhase;
        public InPhaseEnum InPhase => _inPhaseEnum;

        public void AddTurn()
        {
            TurnNumber++;
        }

        public void SetStageData(StageData data)
        {
            Data = data;
        }

        public void SetStagePhase(StagePhase phase)
        {
            _stagePhase = phase;
        }

        public void SetCurrentUnitOnClick(BaseUnitController unit)
        {
            CurrentUnitOnClick = unit;
        }

        public void SetInPhaseEnum(InPhaseEnum inPhaseEnum)
        {
            _inPhaseEnum = inPhaseEnum;
        }
    }
}