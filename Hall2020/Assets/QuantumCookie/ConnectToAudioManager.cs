using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectToAudioManager : MonoBehaviour
{
    private void Awake()
    {
        Slider[] sliders = transform.GetComponentsInChildren<Slider>();
        
        sliders[0].onValueChanged.AddListener(AudioManager.Instance.OnMasterVolumeChanged);
        sliders[1].onValueChanged.AddListener(AudioManager.Instance.OnSfxVolumeChanged);
        sliders[2].onValueChanged.AddListener(AudioManager.Instance.OnMusicVolumeChanged);
    }
}
