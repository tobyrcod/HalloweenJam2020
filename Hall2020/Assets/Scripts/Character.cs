using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 3f;

    [Space]

    [SerializeField] protected float maxHealth;
    [SerializeField] protected SliderController healthSlider;
    protected float currentHealth;

    protected virtual void Awake() {
        healthSlider.SetMaxValue(maxHealth);
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthSlider.SetValue(currentHealth);

        if (currentHealth <= 0f) {
            Die();
        }
    }

    protected virtual void Die() {
        Destroy(this.gameObject);
    }
}
