using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    public Slider healthSlider;
    public Slider damageHealthSlider;
    public float maxHealth = 100f;
    public float health;
    private float lerpSpeed = 0.1f;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (healthSlider.value != damageHealthSlider.value)
        {
            damageHealthSlider.value = Mathf.Lerp(damageHealthSlider.value, health, lerpSpeed);
        }
    }

    public void ChangeHealth(int val)
    {
        maxHealth += val;

        if (val < 0)
        {
            health = Math.Min(health, maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
