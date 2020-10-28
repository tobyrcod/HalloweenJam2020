using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelMeleeController : Weapon
{
    [SerializeField] private Animator anim;

    public void DestroyShovel() {
        GameObject.Destroy(this.gameObject);
        player.isAttacking = false;
    }

    public void SetShovelSpeed(float speed) {
        anim.SetFloat("speed", speed);
    }
}
