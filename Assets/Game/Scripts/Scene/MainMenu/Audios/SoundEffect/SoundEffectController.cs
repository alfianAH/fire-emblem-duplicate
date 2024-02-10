using FireEmblemDuplicate.Core.Singleton;
using FireEmblemDuplicate.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace FireEmblemDuplicate.Scene.MainMenu.Audios.SoundEffect
{
    public class SoundEffectController : Singleton<SoundEffectController>
    {
        [SerializeField] private AudioMixerGroup _sfxAudioMixerGroup;

        private List<AudioSource> _audioSourcePool = new List<AudioSource>();
        private SoundEffectScriptableObject _soundEffect;

        public AudioMixerGroup SfxAudioMixerGroup => _sfxAudioMixerGroup;

        private void Start()
        {
            LoadSoundEffect();
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

        public void Play(PlaySFXMessage message)
        {
            AudioSource audioSource = GetAudioSource();
            SoundEffectConfig soundEffect = GetSoundEffect(message.AudioName);

            audioSource.volume = soundEffect.Volume;
            audioSource.pitch = soundEffect.Pitch;
            audioSource.PlayOneShot(soundEffect.Clip);
            StopAudioCoroutine(audioSource);
        }

        private void LoadSoundEffect()
        {
            _soundEffect = Resources.Load<SoundEffectScriptableObject>("SO/Audio/Sound Effects");
        }

        private SoundEffectConfig GetSoundEffect(string audioName)
        {
            SoundEffectConfig soundEffect = _soundEffect.SoundEffects.Find(
                s => {
                    if (s.Name.ToLower() == audioName.ToLower())
                    {
                        return true;
                    }
                    return false;
                }
            );
            if (soundEffect != null) return soundEffect;

            Debug.LogError($"Audio clip: {audioName} not available");
            return null;
        }

        private AudioSource GetAudioSource()
        {
            AudioSource audioSource = _audioSourcePool.Find(source =>
                !source.isPlaying && !source.gameObject.activeInHierarchy);

            if (audioSource == null)
            {
                GameObject newAudioObject = new GameObject("Sound Effect", typeof(AudioSource));
                newAudioObject.transform.parent = transform;

                // Set mixer
                audioSource = newAudioObject.GetComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = _sfxAudioMixerGroup;

                // Add to pool
                _audioSourcePool.Add(audioSource);
            }

            audioSource.gameObject.SetActive(true);

            return audioSource;
        }

        public void StopAudioCoroutine(AudioSource audioSource)
        {
            StartCoroutine(StopAudio(audioSource));
        }

        /// <summary>
        /// Deactivate audio's game object after not playing anymore
        /// </summary>
        /// <param name="audioSource">Current audio source</param>
        /// <returns></returns>
        private IEnumerator StopAudio(AudioSource audioSource)
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            audioSource.gameObject.SetActive(false);
        }
    }
}