using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Stage;
using FireEmblemDuplicate.Scene.Battle.Stage.Enum;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.Battle.UserInterface
{
    public class BattleUIController : MonoBehaviour
    {
        [SerializeField] private Button _endPhaseButton;
        [SerializeField] private TextMeshProUGUI _turnText;

        private void Start()
        {
            _endPhaseButton.onClick.RemoveAllListeners();
            _endPhaseButton.onClick.AddListener(OnClickEndPhase);
        }

        public void UpdateTurnNumber(UpdateTurnNumberMessage message)
        {
            _turnText.text = $"Turn {message.TurnNumber}";
        }

        private void OnClickEndPhase()
        {
            StagePhase newPhase = StageController.Instance.Stage.Phase == StagePhase.PlayerPhase ? StagePhase.EnemyPhase : StagePhase.PlayerPhase;
            Messenger.Default.Publish(new ChangeStagePhaseMessage(newPhase));
        }
    }
}