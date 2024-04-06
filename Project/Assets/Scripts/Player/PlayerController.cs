using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Collision))]
public class PlayerController : ActorController
{

    public Rigidbody2D PlayerRb;
    public PlayerMovement PlayerMovement;
    public PlayerAnimatorController PlayerAnimator;
    public PlayerAirController PlayerAirController;

  

    public float Speed = 5;

    
    new void Awake()
    {
        base.Awake();
        PlayerAirController = GetComponent<PlayerAirController>();
        Collision.OnGrounded += (isGrounded) =>
        {
            IsGrounded = isGrounded;
            if (isGrounded)
            {
                PlayerAirController.ResetGravity();
                Debug.Log("Grounded");
                if(PlayerRb.velocity.y<=0)
                    PlaySound(PlayerSounds.SPLASH);
                AvailableMechanics.Find(mechanic => mechanic.GetType() == typeof(Jump)).ResetUses();
                AvailableMechanics.Find(mechanic => mechanic.GetType() == typeof(Dash)).ResetUses();
            }
        };
        AudioController = GetComponent<AudioController>();
        
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerAnimator = GetComponent<PlayerAnimatorController>();
        PlayerRb = this.Rigidbody;

        PlayerInput.OnJumpInput += Jump;
        Collision.OnWall += OnWall;
        PlayerInput.OnDashInput += OnDash;
    }

    private void OnDash()
    {
        AvailableMechanics.Find(mechanic => mechanic.GetType() == typeof(Dash)).Call(true);
    }

    private void Jump(bool isJumping)
    {
        AvailableMechanics.Find(mechanic=> mechanic.GetType() ==typeof(Jump)).Call(isJumping);
    }
    private void OnWall(Vector2 wall)
    {
        WallDirection = wall;
    }
    public Vector2 GetWallDirection()
    {
        return WallDirection;
    }
    public void PlaySound(string sound)
    {
        AudioController.PlaySound(sound);
    }

    override public void Die()
    {
        Debug.Log("Player Died");
        FindAnyObjectByType<SceneController>().ReloadScene();
    }

    private void OnDisable()
    {
        PlayerInput.OnJumpInput -= Jump;
        Collision.OnWall -= OnWall;
        PlayerInput.OnDashInput -= OnDash;
        Collision.OnGrounded = null;
    }
}
