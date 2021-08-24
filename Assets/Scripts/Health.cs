using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthPoints = 50;
    
public int HealthPoints
    {
        get { return healthPoints; }
        set
        {
            if (value > 0)
                healthPoints = value;
        }
    }
 
  
    [SerializeField] private Animator animator;

    private void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }
    public void TakeHit(int damage)
    {
        healthPoints -= damage;

        Debug.Log(healthPoints);

        if (animator != null)
            animator.SetTrigger("GetDamage");

        if(healthPoints <= 0)
        {
            Destroy(gameObject);
   //         transform.position = new Vector2(0, 0);
     //       rigidBody.velocity = new Vector2(0, 0);
       //     health = 100;
        }
    }

    public void SetHealth(int bonusHealth)
    {
        healthPoints += bonusHealth;
        if (healthPoints > 100)
            healthPoints = 100;
    }
}
