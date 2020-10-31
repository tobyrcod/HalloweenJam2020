using System;
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

        FinalDialogue();
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

    public GameObject CreateNewEnemy(bool isZombie, Vector2 spawnPosition) {
        GameObject prefab = (isZombie) ? zombiePrefab : batPrefab;
        EnemyAI newEnemy = Instantiate(prefab, spawnPosition, Quaternion.identity, enemyParent).GetComponent<EnemyAI>();
        newEnemy.player = player;
        newEnemy.monster = monster;

        return newEnemy.gameObject;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        SceneManager.LoadScene(1);
    }

    internal void FinalDialogue() {
        Time.timeScale = 0f;
        isGamePaused = true;

        PopUpManager.NewMessageBox(DealWithQuestionResult).Show("Will you make the final sacrifice, your own soul");
    }

    public void DealWithQuestionResult(bool yes) {
        if (yes) {
            PopUpManager.NewMessage(End).Show("That was a mistake");
        }
        else {
            PopUpManager.NewMessage(End).Show("I'm going to take it anyway");
        }
    }

    public void End(bool IGNORE) {
        //Write this function to finish the game, this is triggered by the messgage box
        //You can ignore the parameter im just too lazy right now to remove it!
    }
}
