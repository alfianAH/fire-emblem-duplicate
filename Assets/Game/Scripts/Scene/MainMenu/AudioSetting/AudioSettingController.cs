using FireEmblemDuplicate.Global.AudioData;
using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Utility;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.MainMenu.AudioSetting
{
    public class AudioSettingController : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsScreen;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Button _backButton;
        [SerializeField] private Slider _sfxSlider, _bgmSlider;

        private void Start()
        {
            _backButton.onClick.RemoveAllListeners();
            _backButton.onClick.AddListener(OnClickSettings);

            _sfxSlider.onValueChanged.RemoveAllListeners();
            _sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

            _bgmSlider.onValueChanged.RemoveAllListeners();
            _bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);

            LoadVolume();
        }

        private void OnClickSettings()
        {
            Messenger.Default.Publish(new PlaySFXMessage(AudioName.SFX_BUTTON_PRESSED));
            _settingsScreen.SetActive(false);
        }

        private void LoadVolume()
        {
            _sfxSlider.value = GameAudioController.Instance.GameAudioData.SfxVolume;
            _bgmSlider.value = GameAudioController.Instance.GameAudioData.BgmVolume;
        }

        private void SetAudioMixerVolume(float sliderValue, string parameterName)
        {
            float volume = AudioVolume.ConvertSliderValueToMixerValue(sliderValue);
            _audioMixer.SetFloat(parameterName, volume);
        }

        private void OnSFXVolumeChanged(float value)
        {
            SetAudioMixerVolume(value, AudioVolume.SFX_PARAMETER_NAME);
            Messenger.Default.Publish(new UpdateVolumeMessage(AudioVolume.SFX_VOLUME_TYPE, value));
        }

        private void OnBGMVolumeChanged(float value)
        {
            SetAudioMixerVolume(value, AudioVolume.BGM_PARAMETER_NAME);
            Messenger.Default.Publish(new UpdateVolumeMessage(AudioVolume.BGM_VOLUME_TYPE, value));
        }
    }
}
