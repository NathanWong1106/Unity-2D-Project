using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public float maxHealth;
    public float health;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        slider.maxValue = maxHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealth(float health)
    {
        this.health = health;
        slider.value = health;
    }

    public float GetHealth()
    {
        return health;
    }
}
