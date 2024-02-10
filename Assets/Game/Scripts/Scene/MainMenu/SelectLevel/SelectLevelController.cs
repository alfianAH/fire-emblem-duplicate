using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Utility;
using SuperMaxim.Messaging;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.MainMenu.SelectLevel
{
    public class SelectLevelController : MonoBehaviour
    {
        [SerializeField] private GameObject _selectLevelScreen;
        [SerializeField] private Button _backButton;
        [SerializeField] private Transform _levelParent;
        
        private LevelView _levelPrefab;

        private void Start()
        {
            _backButton.onClick.RemoveAllListeners();
            _backButton.onClick.AddListener(OnClickBack);

            LoadLevelViewPrefab();
            AddLevels();
        }

        private void LoadLevelViewPrefab()
        {
            _levelPrefab = Resources.Load<LevelView>("Prefabs/Level/Level View");
        }

        private void AddLevels()
        {
            List<TextAsset> levelAssets = new List<TextAsset>(Resources.LoadAll<TextAsset>("Data/Level"));

            foreach(TextAsset levelAsset in levelAssets)
            {
                LevelView levelView = Instantiate(_levelPrefab, _levelParent);
                levelView.SetLevel(levelAsset.name);
            }
        }

        private void OnClickBack()
        {
            Messenger.Default.Publish(new PlaySFXMessage(AudioName.SFX_BUTTON_PRESSED));
            _selectLevelScreen.SetActive(false);
        }
    }
}
