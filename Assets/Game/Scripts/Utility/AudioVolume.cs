using UnityEngine;

namespace FireEmblemDuplicate.Utility
{
    public static class AudioVolume
    {
        public const string BGM_PARAMETER_NAME = "bgmVolume";
        public const string SFX_PARAMETER_NAME = "sfxVolume";

        public static float ConvertSliderValueToMixerValue(float sliderValue)
        {
            return Mathf.Log(sliderValue) * 10;
        }
    }
}