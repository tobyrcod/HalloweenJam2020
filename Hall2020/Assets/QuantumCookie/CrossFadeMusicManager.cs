using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFadeMusicManager : MonoBehaviour
  {
        public void ChangeSong(AudioClip newClip) => _targetAudioClip = newClip;
        
        public void Silence() => _targetAudioClip = null;

        [HideInInspector] public float _targetVolume = 1;

        private AudioSource _musicSourceA;
         private AudioSource _musicSourceB;
        private bool _activeIsA;
        
        private AudioSource ActiveAudioSource => _activeIsA ? _musicSourceA : _musicSourceB;
        private AudioSource FadingAudioSource => _activeIsA ? _musicSourceB : _musicSourceA;
        private AudioClip _targetAudioClip;
      
        private void Swap()
        {
            _activeIsA = !_activeIsA;
            ActiveAudioSource.clip = _targetAudioClip;
            ActiveAudioSource.Play();
        }

        private void Lerp(AudioSource src, float targetVolume, float speed)
        {
            float diff = src.volume - targetVolume;

            if (diff == 0)
                return;
            
            float portion = Mathf.Abs(speed * Time.unscaledTime / diff);

            src.volume = Mathf.Lerp(src.volume, targetVolume, portion);
        }

        void Update()
        {
            Lerp(ActiveAudioSource, ActiveAudioSource.clip ? _targetVolume : 0, 1);
            Lerp(FadingAudioSource, 0, 1);

            if (ActiveAudioSource.clip != _targetAudioClip)
            {
                // Swap right away if target song just played and haven't faded out yet
                if (_targetAudioClip == FadingAudioSource.clip)
                {
                    Swap();
                }
                // Swap once fading source is quiet    
                else if (FadingAudioSource.volume < 0.01f)
                        Swap();
            }
        }
        
        private void Awake()
        {
            _musicSourceA = CreateSource("Music Source A");
            _musicSourceB = CreateSource("Music Source B");
        }
        
        private AudioSource CreateSource(string goName)
        {
            var src = new GameObject(goName).AddComponent<AudioSource>();
            src.transform.SetParent(transform);
            src.volume = 0;
            src.loop = true;
            return src;
        }
    }