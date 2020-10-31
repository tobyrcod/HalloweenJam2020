using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAnimator : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.onGameOver += DisableAnim;
    }

    private void DisableAnim()
    {
        GetComponent<Animator>().enabled = false;
    }
}
