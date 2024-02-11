using UnityEngine;

namespace FireEmblemDuplicate.Global.AudioData
{
    public class GameAudioModel : MonoBehaviour
    {
        public GameAudio GameAudioData { get; private set; }

        public void SetGameAudio(GameAudio volume)
        {
            GameAudioData = volume;
        }

        public void SetBgmVolume(float bgmVolume)
        {
            GameAudioData.BgmVolume = bgmVolume;
        }

        public void SetSfxVolume(float sfxVolume)
        {
            GameAudioData.SfxVolume = sfxVolume;
        }
    }
}