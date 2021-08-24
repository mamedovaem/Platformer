using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    [SerializeField] private int damageBonus = 0;
    public int DamageBonus
    {
        get { return damageBonus; }
        set { damageBonus = value; }
    }
    [SerializeField] private bool isDestroyingAfterCollision;
    private IObjectDestroyer destroyer;
    [SerializeField] private GameObject parent;
    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public void Init(IObjectDestroyer destroyer)
    {
        this.destroyer = destroyer;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == parent || GameManager.Instance.obstacleContainer.ContainsKey(collision.gameObject))
            return;
        if (GameManager.Instance.healthContainer.ContainsKey(collision.gameObject))
        {
            var health = GameManager.Instance.healthContainer[collision.gameObject];
            health.TakeHit(Damage + DamageBonus);
        }
        if (isDestroyingAfterCollision)
        {
            if (destroyer == null)
                Destroy(gameObject);
            else
                destroyer.Destroy(gameObject);
        }
    }
}

public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}