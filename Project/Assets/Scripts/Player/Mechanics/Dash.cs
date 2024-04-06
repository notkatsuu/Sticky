using System.Collections;
using UnityEngine;

public class Dash : Mechanic
{
    public delegate void DashDelegate(bool isDashing);
    public static DashDelegate OnDash;

    private int MaxDashes = 1;
    private float DashForce = 10f;
    private float LastHorizontalInput;

    private PlayerInput Input;

    new void Awake()
    {
        base.Awake();
        Input = GetComponent<PlayerInput>();

        MaxUses = MaxDashes;
        UsesLeft = MaxDashes;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.HorizontalInput < 0)
        {
            LastHorizontalInput = -1;
        }
        else if(Input.HorizontalInput > 0)
        {
            LastHorizontalInput = 1;
        }
    }

    protected override void StartCast()
    {
        if (!Controller.IsGrounded && UsesLeft > 0)
        {
            Controller.IsJumping = false;
            Controller.Rigidbody.velocity = Vector2.zero;
            Controller.Rigidbody.AddForce(Vector2.right * LastHorizontalInput * DashForce,ForceMode2D.Impulse);
            Controller.Rigidbody.gravityScale = 0;
            Debug.Log($"New Velocity: {Controller.Rigidbody.velocity}");
            Controller.AudioController.PlaySound(PlayerSounds.DASH);
            Controller.IsDashing = true;
            UsesLeft--;
            OnDash?.Invoke(true);
            StartCoroutine(EndDash());
        }
    }

    private IEnumerator EndDash()
    {
        yield return new WaitForSeconds(0.2f);
        Controller.IsDashing = false;
        Controller.Rigidbody.gravityScale = 3;
        OnDash?.Invoke(false);
    }

    private void OnDestroy()
    {
        OnDash = null;
    }
}
