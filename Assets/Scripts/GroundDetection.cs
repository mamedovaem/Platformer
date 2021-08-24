using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{

    [SerializeField] private bool isGrounded = false;
    public bool IsGrounded
    {
        get { return isGrounded; }
        set
        {
            if (value == true || value == false)
                isGrounded = value;
        }
    }
    
    [SerializeField] private string collisionTag = "Platform";
    public string CollisionTag { get; set; }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
            isGrounded = true;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
            isGrounded = false;

    }
}
