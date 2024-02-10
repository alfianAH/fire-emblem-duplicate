using FireEmblemDuplicate.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.MainMenu.UserInterface
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _partyButton;
        [SerializeField] private Button _settingsButton;

        private void Start()
        {
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(OnClickPlay);
        }

        private void OnClickPlay()
        {
            SceneManager.LoadScene(SceneName.BATTLE_SCENE);
        }
    }
}
