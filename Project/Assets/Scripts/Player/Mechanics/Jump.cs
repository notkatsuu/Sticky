using System.Collections;
using UnityEngine;

public class Jump : Mechanic
{
    public float MaxJumpHeight = 1f;
    public float TimeToJumpApex = 0.4f;
    public int MaxDoubleJumps = 0;
    private float JumpForce;
    new void Awake()
    {
        base.Awake();
        MaxUses = MaxDoubleJumps;
        UsesLeft = MaxDoubleJumps;
    }

    protected override void StartCast()
    {
        if(Controller.IsGrounded)
        {
            NormalJump();
        }
        else
        {
            Vector2 wallDirection = (Controller as PlayerController).GetWallDirection();
            if (wallDirection != Vector2.zero)
            {
                Debug.Log("WallJump");
                WallJump(wallDirection);
                Controller.IsJumping = true;
                
            }
            else
            {
                if (UsesLeft > 0 && (Controller.Rigidbody.velocity.y<=0))
                {
                    NormalJump();
                    UsesLeft--;
                }
            }

        }
    }
    protected override void StopCast()
    {
        Controller.IsJumping = false;
    }

    private void NormalJump()
    {
        JumpForce = MaxJumpHeight / TimeToJumpApex;
        Controller.Rigidbody.velocity = new Vector2(Controller.Rigidbody.velocity.x, 0);
        Controller.Rigidbody.velocity += Vector2.up * JumpForce;
        Controller.IsJumping = true;
        Controller.AudioController.PlaySound(PlayerSounds.JUMP);
    }

    private void WallJump(Vector2 WallDirection)
    {
        Controller.IsWallJumping = true;
        Controller.Rigidbody.velocity = new Vector2(Controller.Rigidbody.velocity.x, 0);
        Controller.Rigidbody.velocity += Vector2.up * JumpForce;
        Controller.Rigidbody.velocity += Vector2.left * WallDirection.x * JumpForce;
        Controller.IsJumping = true;
        Controller.AudioController.PlaySound(PlayerSounds.JUMP);

        StartCoroutine(EndWallJump());
    }
    private IEnumerator EndWallJump()
    {
        yield return new WaitForSeconds(0.2f);
        Controller.IsWallJumping = false;
    }
}