using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton Boilerplate
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null) _instance = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            if(_instance == null) _instance = new GameObject("AudioManager").AddComponent<AudioManager>();
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        
        DontDestroyOnLoad(_instance);

        musicManager = GetComponent<CrossFadeMusicManager>();
    }
    #endregion

    private CrossFadeMusicManager musicManager;
    
    [Range(0, 1)] public float masterVolume = 1f;
    [Range(0, 1)] public float musicVolume = 1f;
    [Range(0, 1)] public float sfxVolume = 1f;

    public void PlaySFX(AudioClip clip)
    {
        PlaySFX(clip, Vector3.zero);
    }

    public void PlaySFX(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position, sfxVolume * masterVolume);
    }

    public void ChangeMusic(AudioClip clip)
    {
        musicManager._targetVolume = musicVolume * masterVolume;
        musicManager.ChangeSong(clip);
    }

    public void OnMasterVolumeChanged(float newValue)
    {
        masterVolume = Mathf.Clamp01(newValue);
        UpdateVolume();
    }
    
    public void OnSfxVolumeChanged(float newValue)
    {
        sfxVolume = Mathf.Clamp01(newValue);
        UpdateVolume();
    }
    
    public void OnMusicVolumeChanged(float newValue)
    {
        musicVolume = Mathf.Clamp01(newValue);
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        musicManager._targetVolume = musicVolume * masterVolume;
    }
}
