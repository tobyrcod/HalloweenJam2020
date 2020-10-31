using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnGameOver : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.onGameOver += DisableThis;
    }

    void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
