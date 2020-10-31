using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPos;
    public bool isZombie;
    public float spawnRate = 5f;
    public float randomness = 1f;

    private void Start() {
        Invoke("Spawn", 10f);

        GameManager.Instance.onGameOver += DisableThis;
    }

    private void Spawn() {
        GameManager.Instance.CreateNewEnemy(isZombie, spawnPos.position);
        float waitTime = Random.Range(spawnRate - randomness, spawnRate + randomness);
        Invoke("Spawn", waitTime);
    }

    private void DisableThis()
    {
        this.enabled = false;
    }
}
