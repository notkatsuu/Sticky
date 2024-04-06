using System.Globalization;
using UnityEngine;
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Collision))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator Animator;

    private const string SpeedY = "SpeedY";
    private const string IsGrounded = "IsGrounded";
    private const string IsMoving = "IsMoving";
    private const string OnWall = "OnWall";
    private const string IsDashing = "IsDashing";

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        PlayerMovement.OnSpeedY += SetSpeedY;
        PlayerMovement.OnMoving += SetIsMoving;
        Collision.OnWall += SetOnWall;
        Dash.OnDash += SetIsDashing;
        Collision.OnGrounded += SetIsGrounded;
    }

    private void SetSpeedY(float speedY)
    {
        Animator.SetFloat(SpeedY, speedY);
    }

    private void SetIsGrounded(bool isGrounded)
    {
        Animator.SetBool(IsGrounded, isGrounded);
    }

    private void SetIsMoving(bool isMoving)
    {
        Animator.SetBool(IsMoving, isMoving);
    }

    private void SetOnWall(Vector2 onWall)
    {
        Animator.SetBool(OnWall, onWall.x!=0);
    }

    private void SetIsDashing(bool isDashing)
    {
        Animator.SetBool(IsDashing, isDashing);
    }

    private void OnDestroy()
    {
        PlayerMovement.OnSpeedY -= SetSpeedY;
        PlayerMovement.OnMoving -= SetIsMoving;
        Collision.OnWall -= SetOnWall;
        Dash.OnDash -= SetIsDashing;
        Collision.OnGrounded -= SetIsGrounded;
        Animator = null;
    }

}