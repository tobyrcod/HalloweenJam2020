using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

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
        
        Animprefix();
    }
    #endregion

    public bool isGamePaused = false;
    public bool isGameOver = false;

    [SerializeField] PlayerController player;
    [SerializeField] Character monster;
    [SerializeField] Transform enemyParent;
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] GameObject batPrefab;

    public delegate void GeneralEvent();

    public event GeneralEvent onTogglePause;
    public event GeneralEvent onGameOver;

    private void Start()
    {
        onGameOver += FinalDialogue;
        //OnGameOver();
    }

    public void OnGameOver()
    {
        Animhotfix();
        
        isGameOver = true;
        onGameOver?.Invoke();
    }

    void Animprefix()
    {
        Animator[] anims = GameObject.FindObjectsOfType<Animator>();

        for (int i = 0; i < anims.Length; i++)
        {
            if(anims[i].transform.root.CompareTag("Player") && !anims[i].gameObject.CompareTag("Player")) continue;
            anims[i].enabled = false;
        }
    }
    
    void Animhotfix()
    {
        Animator[] anims = GameObject.FindObjectsOfType<Animator>();

        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].enabled = true;
        }
    }

    public void OnTogglePause()
    {
        isGamePaused = !isGamePaused;
        onTogglePause?.Invoke();
    }

    public GameObject CreateNewEnemy(bool isZombie, Vector2 spawnPosition)
    {
        GameObject prefab = (isZombie) ? zombiePrefab : batPrefab;
        EnemyAI newEnemy = Instantiate(prefab, spawnPosition, Quaternion.identity, enemyParent).GetComponent<EnemyAI>();
        newEnemy.player = player;
        newEnemy.monster = monster;

        return newEnemy.gameObject;
    }

    public void Restart()
    {
        OnTogglePause();
        SceneManager.LoadScene(1);
    }

    internal void FinalDialogue() {
        Time.timeScale = 0f;
        isGamePaused = true;

        PopUpManager.NewMessageBox(DealWithQuestionResult).Show("Proceed to give life, to your Ultimate Creation?");
    }

    public void DealWithQuestionResult(bool yes) {
        if (yes) {
            PopUpManager.NewMessage(End).Show("No, wait, that was a mis-");
        }
        else {
            PopUpManager.NewMessage(End).Show("I'm going to do it anyway");
        }
    }

    public void End(bool IGNORE)
    {
        Time.timeScale = 1f;
        isGamePaused = false;

        //Write this function to finish the game, this is triggered by the messgage box
        //You can ignore the parameter im just too lazy right now to remove it!
        
        GetComponent<PlayableDirector>().Play();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
