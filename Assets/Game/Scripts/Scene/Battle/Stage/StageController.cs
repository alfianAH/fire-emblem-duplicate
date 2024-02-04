using FireEmblemDuplicate.Core.Singleton;
using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Stage.Enum;
using FireEmblemDuplicate.Scene.Battle.Unit;
using SuperMaxim.Messaging;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Stage
{
    public class StageController : Singleton<StageController>
    {
        [SerializeField] private Rect _allowedArea;
        [SerializeField] private StagePhase _stagePhase;
        [SerializeField] private InPhaseEnum _inPhaseEnum;
        
        public StageData Data { get; private set; }
        public BaseUnitController CurrentUnitOnClick { get; private set; }
        public StagePhase Phase => _stagePhase;
        public InPhaseEnum InPhase => _inPhaseEnum;
        public Rect AllowedArea => _allowedArea;

        private void Awake()
        {
            _stagePhase = StagePhase.PlayerPhase;
            _inPhaseEnum = InPhaseEnum.Idle;
            ReadLevelResource("Level 1");
        }

        private void Start()
        {
            ProcessStageData();
        }

        public void SetCurrentUnitOnClick(ChangeCurrentUnitOnClickMessage message)
        {
            CurrentUnitOnClick = message.UnitController;
        }

        public void SetInPhaseEnum(ChangeStageInPhaseMessage message)
        {
            _inPhaseEnum = message.InPhase;
        }

        private void ReadLevelResource(string fileName)
        {
            TextAsset stageTextAsset = Resources.Load<TextAsset>($"Data/Level/{fileName}");
            Data = JsonUtility.FromJson<StageData>(stageTextAsset.text);
        }

        private void ProcessStageData()
        {
            Messenger.Default.Publish(new MakeTerrainMessage(Data.TerrainPool));
        }
    }
}
