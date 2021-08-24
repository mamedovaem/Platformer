using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject leftBorder;
    public GameObject LeftBorder { get
        {
            return leftBorder;
        }
        }
    [SerializeField] private GameObject rightBorder;
    public GameObject RightBorder { get { return rightBorder; }  }
    [SerializeField] private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody
    {
        get { return rigidBody; }
        set
        {
            if (value != null)
                rigidBody = value;
        }
    }

    public float speed = 1F;
    public bool isRightDirection = false;
    public GroundDetection groundDetection;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    [SerializeField] private CollisionDamage collisionDamage;

    private void Update()
    {

        if (groundDetection.IsGrounded)
        {
            if (transform.position.x > rightBorder.transform.position.x || collisionDamage.Direction < 0)
                isRightDirection = false;
            else if (transform.position.x < leftBorder.transform.position.x || collisionDamage.Direction > 0)
                isRightDirection = true;

            RigidBody.velocity = isRightDirection ? Vector2.right : Vector2.left;
            RigidBody.velocity *= speed;
        }

        if (RigidBody.velocity.x > 0)
            spriteRenderer.flipX = true;
        else if(RigidBody.velocity.x < 0)
            spriteRenderer.flipX = false;

    }
}
