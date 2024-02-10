using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Utility;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.MainMenu.UserInterface
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private GameObject _selectLevelScreen;

        private void Start()
        {
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(OnClickPlay);
        }

        private void OnClickPlay()
        {
            Messenger.Default.Publish(new PlaySFXMessage(AudioName.SFX_BUTTON_PRESSED));
            _selectLevelScreen.SetActive(true);
        }
    }
}
