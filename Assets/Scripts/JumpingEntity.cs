using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEntity : PhysicsComponent
{
    [SerializeField]
    protected Transform groundCheck_Left;//A point to check if we are grounded
    [SerializeField]
    protected Transform groundCheck_Right;//A point to check if we are grounded
    [SerializeField]
    protected float groundRadius = 0.2f;//The radius to check around the groundCheck points
    [SerializeField]
    protected LayerMask whatIsGround;//The layer that is Ground (we can only be grounded here)
    [SerializeField]
    protected LayerMask whatIsPlayer;//The layer that is Player (we can only be grounded here)

    public bool IsGrounded = true;

    /// <summary>
    /// Checks if the object is grounded by checking the groundCheck points for collissions. 
    /// Use this at the beggining of the frame, then IsGrounded for subsequent checks for better optimization.
    /// </summary>
    public bool CheckIsGrounded()
    {
        Collider2D LeftCheck = Physics2D.OverlapCircle(groundCheck_Left.position, groundRadius, whatIsGround, ~whatIsPlayer);
        Collider2D RightCheck = Physics2D.OverlapCircle(groundCheck_Right.position, groundRadius, whatIsGround, ~whatIsPlayer);
        if (LeftCheck || RightCheck)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }

        return IsGrounded;
    }

    /// <summary>
    /// Makes the object jump if the object has a Rigidbody2D by applying force.
    /// </summary>
    /// <param name="jumpForce">How much force to use</param>
    public void Jump(float jumpForce = 600.0f, bool checkIfGrounded = true)
    {
        if(checkIfGrounded)
        {
            if (IsGrounded)
                RB.AddForce(new Vector2(0, jumpForce));
        }
        else
        {
            RB.AddForce(new Vector2(0, jumpForce));
        }
    }
}