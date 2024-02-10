using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.MainMenu.Audios.SoundEffect
{
    [Serializable]
    public class SoundEffectConfig : Sound
    {

    }

    [CreateAssetMenu(fileName = "Sound Effect", menuName = "SO/Audio/SFX")]
    public class SoundEffectScriptableObject : ScriptableObject
    {
        [SerializeField] private List<SoundEffectConfig> _soundEffects;

        public List<SoundEffectConfig> SoundEffects => _soundEffects;
    }
}