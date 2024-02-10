using FireEmblemDuplicate.Core.Singleton;
using FireEmblemDuplicate.Utility;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.MainMenu.Audios.BackgroundMusic
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusicController : Singleton<BackgroundMusicController>
    {
        private AudioSource _audioSource;
        private BackgroundMusicScriptableObject _backgroundMusic;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            LoadBackgroundMusic();
            Play(AudioName.BGM);

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
        }

        public void Play(string audioName)
        {
            BackgroundMusicConfig bgm = GetBackgroundMusic(audioName);

            _audioSource.clip = bgm.Clip;
            _audioSource.loop = bgm.IsLoop;
            _audioSource.volume = bgm.Volume;
            _audioSource.pitch = bgm.Pitch;
            _audioSource.Play();
        }

        private void LoadBackgroundMusic()
        {
            _backgroundMusic = Resources.Load<BackgroundMusicScriptableObject>("SO/Audio/Background Musics");
        }

        private BackgroundMusicConfig GetBackgroundMusic(string audioName)
        {
            BackgroundMusicConfig backgroundMusic = _backgroundMusic.BackgroundMusics.Find(
                b => {
                    if (b.Name.ToLower() == audioName.ToLower())
                    {
                        return true;
                    }
                    return false;
                }
            );
            if (backgroundMusic != null) return backgroundMusic;

            Debug.LogError($"Audio clip: {audioName} not available");
            return null;
        }
    }
}