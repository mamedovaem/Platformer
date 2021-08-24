using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitHealthAmount : MonoBehaviour
{
    [SerializeField] private int healAmount;
    public int HealAmount
    {
        get { return healAmount; }
        set
        {
            if (value >= 0)
                healAmount = value;
        }
    }
  [SerializeField] private  string collisionTag = "Player";
  public string CollisionTag { get; set; }
    [SerializeField] private Health playerHealth;
    public Health PlayerHealth
    {
        get { return playerHealth; }
        set
        {
            if (value != null)
                playerHealth = value;
        }
    }

    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
        {
            PlayerHealth = collision.gameObject.GetComponent<Health>();
            PlayerHealth.SetHealth(HealAmount);
            
            Debug.Log("Player's health: " + PlayerHealth.HealthPoints);

            animator.SetTrigger("StartDestroy");
            
        }
    }

    public void EndDestroy()
    {
        Destroy(gameObject);
    }
}
