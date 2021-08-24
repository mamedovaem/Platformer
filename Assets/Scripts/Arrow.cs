using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float force;
    [SerializeField] private Player player;

    public float Force
    {
        get
        {
            return force;
        }
        set
        {
            force = value;
        }
    }
[SerializeField] private TriggerDamage triggerDamage;
    public TriggerDamage TriggerDamage
    {
        get { return triggerDamage; }
        set { triggerDamage = value; }
    }
    [SerializeField] private float lifeTime;

    public void Destroy(GameObject gameObject)
    {
        player.ReturnArrowToPool(this);
    }

    public void SetImpulse(Vector2 direction, float force, Player player)
    {
        this.player = player;
   
        triggerDamage.Init(this);
        
        triggerDamage.Parent = player.gameObject;
        rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        if (force < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(StartLife());
    }

    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;
    }
}

