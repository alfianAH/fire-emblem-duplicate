using FireEmblemDuplicate.Core.Singleton;
using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Stage.Enum;
using SuperMaxim.Messaging;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Stage
{
    public class StageController : Singleton<StageController>
    {
        [SerializeField] private Rect _allowedArea;
        
        public StageModel Stage { get; private set; }
        public Rect AllowedArea => _allowedArea;

        private void Awake()
        {
            Stage = GetComponent<StageModel>();
            ReadLevelResource("Level 1");
        }

        private void Start()
        {
            ProcessStageData();
        }

        public void SetCurrentUnitOnClick(ChangeCurrentUnitOnClickMessage message)
        {
            Stage.SetCurrentUnitOnClick(message.UnitController);
        }

        public void SetInPhaseEnum(ChangeStageInPhaseMessage message)
        {
            Stage.SetInPhaseEnum(message.InPhase);
        }

        private void ReadLevelResource(string fileName)
        {
            TextAsset stageTextAsset = Resources.Load<TextAsset>($"Data/Level/{fileName}");
            Stage.SetStageData(JsonUtility.FromJson<StageData>(stageTextAsset.text));
        }

        private void ProcessStageData()
        {
            Messenger.Default.Publish(new MakeTerrainMessage(Stage.Data.TerrainPool));
        }
    }
}
