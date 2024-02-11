using FireEmblemDuplicate.Core.Singleton;
using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Utility;
using System.IO;
using UnityEngine;

namespace FireEmblemDuplicate.Global.AudioData
{
    public class GameAudioController : Singleton<GameAudioController>
    {
        private GameAudioModel _model;

        public GameAudio GameAudioData => _model.GameAudioData;

        private void Awake()
        {
            _model = GetComponent<GameAudioModel>();
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            Load();
        }

        public void OnSliderValueChanged(UpdateVolumeMessage message)
        {
            switch (message.VolumeType)
            {
                case AudioVolume.BGM_VOLUME_TYPE:
                    _model.SetBgmVolume(message.Volume);
                    break;

                case AudioVolume.SFX_VOLUME_TYPE:
                    _model.SetSfxVolume(message.Volume);
                    break;
            }

            Save();
        }

        private void Load()
        {
            string directory = Path.Combine(Application.persistentDataPath, "Save");
            string path = Path.Combine(directory, "GameAudio.json");

            if (File.Exists(path))
            {
                string gameAudioFile = File.ReadAllText(path);
                GameAudio gameAudioData = JsonUtility.FromJson<GameAudio>(gameAudioFile);
                _model.SetGameAudio(gameAudioData);
            }
            else
            {
                Directory.CreateDirectory(directory);
                InitGameAudio();
            }
        }

        private void InitGameAudio()
        {
            TextAsset initProgressFile = Resources.Load<TextAsset>("Data/GameAudio/InitialGameAudio");
            GameAudio initGameAudio = JsonUtility.FromJson<GameAudio>(initProgressFile.text);
            _model.SetGameAudio(initGameAudio);
            Save();
        }

        private void Save()
        {
            string path = $"{Application.persistentDataPath}/Save/GameAudio.json";
            string gameAudioData = JsonUtility.ToJson(_model.GameAudioData);
            File.WriteAllText(path, gameAudioData);
        }
    }
}
