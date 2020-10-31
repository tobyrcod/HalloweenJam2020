using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_PauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.onTogglePause += TogglePause;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !GameManager.Instance.isGameOver)
        {
            GameManager.Instance.OnTogglePause();
            Debug.Log("Pause Toggled");
        }
    }

    private void TogglePause()
    {
        Time.timeScale = GameManager.Instance.isGamePaused ? 0 : 1;
    }

    private void OnDisable()
    {
        GameManager.Instance.onTogglePause -= TogglePause;
    }
}
