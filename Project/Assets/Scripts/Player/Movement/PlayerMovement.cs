using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{

    public delegate void MovingDelegate(bool isMoving);

    public delegate void SpeedYDelegate(float speedY);

    public static SpeedYDelegate OnSpeedY;

    public static MovingDelegate OnMoving;

    //remove this when pickup system is implemented

    public float Speed;

    private PlayerInput Input;

    private Rigidbody2D PlayerRb;
    private PlayerController PlayerController;

    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
        PlayerController = GetComponent<PlayerController>();
        PlayerRb = PlayerController.PlayerRb;
        Speed = PlayerController.Speed;
    }
    private void Update()
    {
        if(!PlayerController.IsWallJumping&&!PlayerController.IsDashing)
            Movement();
    }

    private void FixedUpdate()
    {
        OnSpeedY?.Invoke(PlayerRb.velocity.y);
        

        if (PlayerRb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (PlayerRb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Movement()
    {
        PlayerRb.velocity = new Vector2(Vector2.right.x * Input.HorizontalInput * Speed, PlayerRb.velocity.y);
        PlayerController.IsMoving = Mathf.Abs(PlayerRb.velocity.x) > 0.01f;
        OnMoving?.Invoke(PlayerController.IsMoving);
        if (PlayerController.IsMoving&&PlayerController.IsGrounded&&!PlayerController.IsJumping)
        {
            if(!PlayerController.AudioController.isPlaying())PlayerController.AudioController.PlaySound(PlayerSounds.MOVE);
        }
    }

}