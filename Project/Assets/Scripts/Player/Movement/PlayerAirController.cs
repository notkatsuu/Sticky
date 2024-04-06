using System;
using UnityEngine;


public class PlayerAirController : MonoBehaviour
{
    private PlayerController PlayerController;
    private Rigidbody2D PlayerRb;
    private PlayerInput Input;

    public float Speed;
    public float FallMultiplier = 3f;
    public float LowJumpMultiplier = 9f;
    public float MaxFallSpeed = 50f;
    public float HangTimeThreshold = 0.3f;

    private bool ShouldSlide;
    private bool ShouldFallFaster;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerRb = PlayerController.PlayerRb;
        PlayerRb.gravityScale = FallMultiplier;
        Input = GetComponent<PlayerInput>();
        Speed = PlayerController.Speed;
    }

    // Update is called once per frame
    void Update()
    {

            
        if (!(PlayerController.IsGrounded||PlayerController.IsDashing))
        {
            PlayerRb.velocity = new Vector2(Mathf.Clamp(PlayerRb.velocity.x, -Speed, Speed), Mathf.Max(PlayerRb.velocity.y, -MaxFallSpeed));

            if(PlayerRb.velocity.y<0)
            {
                ShouldSlide = PlayerController.GetWallDirection().x != 0 && Input.HorizontalInput == PlayerController.GetWallDirection().x;
                if (ShouldSlide)
                {
                    PlayerController.IsJumping = false;
                    PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, Mathf.Max(PlayerRb.velocity.y, -4));
                }
                PlayerRb.gravityScale = FallMultiplier;
                return;
            }
            ShouldFallFaster = PlayerRb.velocity.y > 0 && (!PlayerController.IsJumping);
            if(ShouldFallFaster)
            {
                PlayerRb.gravityScale = LowJumpMultiplier;
                return;
            }
        }
    }

    public void ResetGravity()
    {
        PlayerRb.gravityScale = FallMultiplier;
    }

}
