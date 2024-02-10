using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Stage;
using FireEmblemDuplicate.Scene.Battle.Stage.Enum;
using FireEmblemDuplicate.Scene.Battle.Unit.Enum;
using FireEmblemDuplicate.Utility;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.Battle.UserInterface
{
    public class BattleUIController : MonoBehaviour
    {
        [Header("Gameplay")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _endPhaseButton;
        [SerializeField] private TextMeshProUGUI _turnText;

        [Header("Game over")]
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private TextMeshProUGUI _gameOverTitle;
        [SerializeField] private Button _homeButton;

        private void Start()
        {
            _endPhaseButton.onClick.RemoveAllListeners();
            _endPhaseButton.onClick.AddListener(OnClickEndPhase);

            _homeButton.onClick.RemoveAllListeners();
            _homeButton.onClick.AddListener(LoadHome);

            _fightButton.onClick.RemoveAllListeners();
            _fightButton.onClick.AddListener(StartFight);

            _endPhaseButton.gameObject.SetActive(false);
        }

        public void UpdateTurnNumber(UpdateTurnNumberMessage message)
        {
            _turnText.text = $"Turn {message.TurnNumber}";
        }

        public void OnWin(WinMessage message)
        {
            _gameOverScreen.SetActive(true);

            switch (message.WinningSide)
            {
                case UnitSide.Player:
                    _gameOverTitle.text = "Player win";
                    break;

                case UnitSide.Enemy:
                    _gameOverTitle.text = "Enemy win";
                    break;
            }
        }

        private void LoadHome()
        {
            Messenger.Default.Publish(new PlaySFXMessage(AudioName.SFX_BUTTON_PRESSED));
            SceneManager.LoadScene(SceneName.MAIN_MENU_SCENE);
        }

        private void OnClickEndPhase()
        {
            Messenger.Default.Publish(new PlaySFXMessage(AudioName.SFX_BUTTON_PRESSED));
            StagePhase newPhase = StageController.Instance.Stage.Phase == StagePhase.PlayerPhase ? StagePhase.EnemyPhase : StagePhase.PlayerPhase;
            Messenger.Default.Publish(new ChangeStagePhaseMessage(newPhase));
        }

        private void StartFight()
        {
            Messenger.Default.Publish(new PlaySFXMessage(AudioName.SFX_BUTTON_PRESSED));
            _endPhaseButton.gameObject.SetActive(true);
            _fightButton.gameObject.SetActive(false);
            Messenger.Default.Publish(new ChangeStagePhaseMessage(StagePhase.PlayerPhase));
        }
    }
}