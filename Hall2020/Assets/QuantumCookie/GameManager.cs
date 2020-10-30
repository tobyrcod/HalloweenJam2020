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
        SceneManager.LoadScene(1);
    }
}
