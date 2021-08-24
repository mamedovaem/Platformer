using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{ [SerializeField] private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    [SerializeField] private bool isDestroyingAfterCollision;
    private IObjectDestroyer destroyer;

    public void Init(IObjectDestroyer destroyer)
    {
        this.destroyer = destroyer;
    }

    private void Start()
    {
        GameManager.Instance.obstacleContainer.Add(gameObject, this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (GameManager.Instance.player.gameObject == collision.gameObject)
        {
            var health = GameManager.Instance.healthContainer[collision.gameObject];
            health.TakeHit(Damage);
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

