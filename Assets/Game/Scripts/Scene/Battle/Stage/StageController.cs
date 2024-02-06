using FireEmblemDuplicate.Core.Singleton;
using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Stage.Enum;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Stage
{
    public class StageController : Singleton<StageController>
    {
        [SerializeField] private Rect _allowedArea;
        
        [Header("User Interface")]
        [SerializeField] private TextMeshProUGUI _playerPhaseText;
        [SerializeField] private TextMeshProUGUI _enemyPhaseText;
        [SerializeField, Range(20, 50)] private int _activeFontSize, _inactiveFontSize;

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
            SetPhase(StagePhase.PlayerPhase);
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

        private void SetPhase(StagePhase newPhase)
        {
            switch (newPhase)
            {
                case StagePhase.PlayerPhase:
                    SetPhaseActive(_playerPhaseText);
                    SetPhaseInactive(_enemyPhaseText);
                    break;

                case StagePhase.EnemyPhase:
                    SetPhaseActive(_enemyPhaseText);
                    SetPhaseInactive(_playerPhaseText);
                    break;
            }
        }

        private void SetPhaseActive(TextMeshProUGUI text)
        {
            text.alpha = 1f;
            text.fontSize = _activeFontSize;
            text.fontStyle = FontStyles.Bold;
        }

        private void SetPhaseInactive(TextMeshProUGUI text)
        {
            text.alpha = 0.4f;
            text.fontSize = _inactiveFontSize;
            text.fontStyle = FontStyles.Normal;
        }
    }
}
