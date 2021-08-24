using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    [SerializeField] private float delta;
    private float healthValue;
    private float currentHealth;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>(); 
        healthValue = (player.Health.HealthPoints) / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = (player.Health.HealthPoints) / 100.0f;
        if (currentHealth > healthValue)
            healthValue += delta;
        if (currentHealth < healthValue)
            healthValue -= delta;
        if (Mathf.Abs(currentHealth - healthValue) < delta)
            healthValue = currentHealth;
        health.fillAmount = healthValue;
    }
}
