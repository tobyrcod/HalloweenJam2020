using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_PauseUI : MonoBehaviour
{
    public GameObject pauseUI;
    
    private void OnEnable()
    {
        GameManager.Instance.onTogglePause += TogglePauseUI;
    }

    private void TogglePauseUI()
    {
        pauseUI.SetActive(GameManager.Instance.isGamePaused ? true : false);
    }

    private void OnDisable()
    {
        GameManager.Instance.onTogglePause -= TogglePauseUI;
    }
}
