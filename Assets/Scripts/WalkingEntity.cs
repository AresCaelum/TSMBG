using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEntity : PhysicsComponent
{
    /// <summary>
    /// Moves the object either left or right depending on the direction ()
    /// </summary>
    /// <param name="direction">Negative is Left and Positive is Right, value is a float so momentum can be used</param>
    /// <param name="speed">Units per second the object will move at</param>
    public void Move(float direction, float speed)
    {
        RB.velocity = new Vector2(direction * speed, RB.velocity.y);
    }

    /// <summary>
    /// Stops horizontal all movement of the object.
    /// </summary>
    public void StopHorizontalMovement()
    {
        RB.velocity = new Vector2(0.0f, RB.velocity.y);
    }

    /// <summary>
    /// Stops horizontal all movement of the object.
    /// </summary>
    public void StopVerticalMovement()
    {
        RB.velocity = new Vector2(RB.velocity.x, 0.0f);
    }
}