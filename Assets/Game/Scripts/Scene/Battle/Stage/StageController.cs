using FireEmblemDuplicate.Core.Singleton;
using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Stage.Enum;
using FireEmblemDuplicate.Scene.Battle.Unit;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Stage
{
    public class StageController : Singleton<StageController>
    {
        [SerializeField] private Rect _allowedArea;
        [SerializeField] private StagePhase _stagePhase;
        [SerializeField] private InPhaseEnum _inPhaseEnum;
        
        public BaseUnitController CurrentUnitOnClick { get; private set; }
        public StagePhase Phase => _stagePhase;
        public InPhaseEnum InPhase => _inPhaseEnum;
        public Rect AllowedArea => _allowedArea;

        private void Awake()
        {
            _stagePhase = StagePhase.PlayerPhase;
            _inPhaseEnum = InPhaseEnum.Idle;
        }

        public void SetCurrentUnitOnClick(ChangeCurrentUnitOnClickMessage message)
        {
            CurrentUnitOnClick = message.UnitController;
        }

        public void SetInPhaseEnum(ChangeStageInPhaseMessage message)
        {
            _inPhaseEnum = message.InPhase;
        }
    }
}
