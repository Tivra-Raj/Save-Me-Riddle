using System;
using UnityEngine;

namespace Sound
{
    public class SoundView : MonoBehaviour
    {
        [SerializeField] private AudioSource audioEffects;
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private Sounds[] audioList;

        private void Start() => PlayBackgroundMusic(SoundType.BackgroundMusic, true);

        public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioEffects.loop = loopSound;
                audioEffects.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }

        public void PlayBackgroundMusic(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                backgroundMusic.loop = loopSound;
                backgroundMusic.clip = clip;
                backgroundMusic.Play();
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }

        private AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds sound = Array.Find(audioList, item => item.SoundType == soundType);
            if (sound != null)
            {
                return sound.Audio;
            }
            return null;
        }
    }
}