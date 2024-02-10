using System;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.MainMenu.Audios
{
    [Serializable]
    public class Sound
    {
        [SerializeField] private string _name;
        [SerializeField] private AudioClip _clip;

        [Range(0, 1)]
        [SerializeField] private float _volume = 1;
        [Range(-3, 3)]
        [SerializeField] private float _pitch = 1;

        public string Name => _name;
        public AudioClip Clip => _clip;
        public float Volume => _volume;
        public float Pitch => _pitch;
    }
}
