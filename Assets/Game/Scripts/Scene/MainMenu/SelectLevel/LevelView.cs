using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Utility;
using SuperMaxim.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.MainMenu.SelectLevel
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private Button _loadSceneButton;

        private string _levelName;

        public void SetLevel(string name)
        {
            _levelName = name;
            _levelNameText.text = _levelName;
            _loadSceneButton.onClick.RemoveAllListeners();
            _loadSceneButton.onClick.AddListener(LoadBattleScene);
        }

        private void LoadBattleScene()
        {
            Messenger.Default.Publish(new PlaySFXMessage(AudioName.SFX_BUTTON_PRESSED));
            SelectedLevel.Instance.SetLevelName(_levelName);
            SceneManager.LoadScene(SceneName.BATTLE_SCENE);
        }
    }
}
