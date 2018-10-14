using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WalkingEntity))]
[RequireComponent(typeof(JumpingEntity))]
public class Player : MonoBehaviour
{
    WalkingEntity walkingEntity;
    JumpingEntity jumpingEntity;

    int MovingDirection = 1;
    float Direction = 0;
    [SerializeField]
    float Speed = 10.0f;
    [SerializeField]
    float JumpForce = 600.0f;
    [SerializeField]
    float MaxJumpSpeed = 15.0f;

    [SerializeField]
    bool AirSpeedLock = false;//Object's speed doesn't or does decrease when in the air.
    [SerializeField]
    bool AirMovementLock = false; //Object can or cannot control their direction in the air
    [SerializeField]
    bool UI_Controls = true;

    [SerializeField]
    public static int MaxHealth = 10;
    public static int Health;
    float HealthRefreshRate = 2.5f;
    [SerializeField]
    Image HealthBar = null;

    IEnumerator Start()
    {
        walkingEntity = GetComponent<WalkingEntity>();
        jumpingEntity = GetComponent<JumpingEntity>();
        GetComponent<Rigidbody2D>().gravityScale = 3.5f;
        Health = MaxHealth;

        while (true)
        {
            if (Health < MaxHealth)
            {
                yield return new WaitForSeconds(HealthRefreshRate);
                Health++;
                if (Health > MaxHealth)
                    Health = MaxHealth;
                HealthBar.fillAmount = (float)Health / (float)MaxHealth;
            }
            else
            {
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().gravityScale = 3.5f;
        //Set the grounded bool
        jumpingEntity.CheckIsGrounded();

        //If grounded and not going up, stop falling
        if (jumpingEntity.IsGrounded && GetComponent<Rigidbody2D>().velocity.y <= 0.0f)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            walkingEntity.StopVerticalMovement();
        }

        //Check for Input
        CheckInput();

        //Move entity according to Input.
        walkingEntity.Move(Direction, Speed);

        if (GetComponent<Rigidbody2D>().velocity.y > MaxJumpSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, MaxJumpSpeed);
    }

    public void MoveRight()
    {
        if (jumpingEntity.IsGrounded)
        {
            if (!AirSpeedLock)
                walkingEntity.StopHorizontalMovement();

            Direction = 1.0f;
        }
        else if (!AirMovementLock)
        {
            Direction = 1.0f;
        }
    }

    public void MoveLeft()
    {
        if (jumpingEntity.IsGrounded)
        {
            if (!AirSpeedLock)
                walkingEntity.StopHorizontalMovement();

            Direction = -1.0f;
        }
        else if (!AirMovementLock)
        {
            Direction = -1.0f;
        }
    }

    public void StopMoving()
    {
        Direction = 0.0f;
        walkingEntity.StopHorizontalMovement();
    }

    public void Jump()
    {
        jumpingEntity.Jump(JumpForce);
    }

    void CheckInput()
    {
        if (UI_Controls)
            return;

        if (!AirSpeedLock)
            Direction = 0.0f;

        //Facing direction.
        if (Input.GetAxisRaw("Horizontal") != 0.0f || MovingDirection == 0)
            MovingDirection = Direction > 0.0f ? 1 : -1;

        //Jumping behaviour
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpingEntity.Jump(JumpForce);
        }

        //Moving behaviour while Grounded or not Grounded
        if (jumpingEntity.IsGrounded)
        {
            if (!AirSpeedLock)
                walkingEntity.StopHorizontalMovement();

            Direction = Input.GetAxisRaw("Horizontal");
        }
        else if (!AirMovementLock)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                Direction = 1.0f;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                Direction = -1.0f;
            }
        }
    }

    public void TakeOneHealth()
    {
        Health--;
        HealthBar.fillAmount = (float)Health / (float)MaxHealth;
        if (Health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        AudioSource audioPlayer = GameObject.Find("RythmnSpawner").GetComponent<AudioSource>();
        audioPlayer.Stop();
        WindowManager.Instance.SaveAttempts();
        WindowManager.Instance.CreateFailureWindow();
        Destroy(this);
    }
}