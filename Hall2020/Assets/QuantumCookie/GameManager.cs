using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    #region Singleton Boilerplate
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null) _instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            if(_instance == null) _instance = new GameObject("GameManager").AddComponent<GameManager>();
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
    }
    #endregion

    public bool isGamePaused = false;
    public bool isGameOver = false;


    public delegate void GeneralEvent();

    public event GeneralEvent onTogglePause;
    public event GeneralEvent onGameOver;
    
    public void OnGameOver()
    {
        isGameOver = true;
        onGameOver?.Invoke();
    }

    public void OnTogglePause()
    {
        isGamePaused = !isGamePaused;
        onTogglePause?.Invoke();
    }
}
