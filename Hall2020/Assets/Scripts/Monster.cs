using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{

    public GameObject diedCancas;

    protected override void Die() {
        diedCancas.SetActive(true);
        Time.timeScale = 0f;
    }

}
