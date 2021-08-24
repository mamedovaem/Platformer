using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    public int Damage
    {
        get { return damage; }
        set
        {
            if (value >= 0)
                damage = value;
        }
    }

    [SerializeField] private Animator animator;
    public Animator Animator
    {
        get
        { return animator; }
        set
        {
            if (value != null)
                animator = value;
        }
    }

    private Health health;
    private float direction = 0;
    public float Direction
    {
        get { return direction; }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        //     health = collision.gameObject.GetComponent<Health>();
        if (GameManager.Instance.healthContainer.ContainsKey(collision.gameObject))
        {
            health = GameManager.Instance.healthContainer[collision.gameObject];

            direction = (collision.transform.position - transform.position).x;
            animator.SetFloat("Direction", Mathf.Abs(direction));
        }

    }

    public void SetDamage()
    {
        if (health != null)
            health.TakeHit(damage);
        health = null;
        direction = 0;
        animator.SetFloat("Direction", 0f);
    }
}
