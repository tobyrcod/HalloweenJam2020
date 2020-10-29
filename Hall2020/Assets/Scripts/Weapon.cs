using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damageMult;
    [SerializeField] private float knockbackMult;
   
    [SerializeField] private float damageFlat;
    [SerializeField] private float knockbackFlat;

    public float damage { get { return damageFlat * damageMult; } }
    public float knockback{ get { return knockbackFlat * knockbackMult; } }

    public PlayerController player;

    public virtual void Init(PlayerController player) {
        this.player = player;
    }
}
