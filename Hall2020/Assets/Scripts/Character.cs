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

    public void ChangeHealth(float amount) {
        currentHealth += amount;
        healthSlider.SetValue(currentHealth);

        if (currentHealth <= 0f) {
            Die();
        }
    }

    public int HealRandomAmount() {
        int random = Random.Range(0, 11);
        if (random >= 3) {
            ChangeHealth(random * 2);
            return random;
        }

        return 0;
    }

    protected virtual void Die() {
        Destroy(this.gameObject);
    }
}
