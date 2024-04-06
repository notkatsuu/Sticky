using UnityEngine;

public class Collision : MonoBehaviour
{
    public delegate void GroundedDelegate(bool isGrounded);

    public delegate void LeftWallDelegate(Vector2 side);

    private static readonly float ColliderDistance = 0.5f;
    public static GroundedDelegate OnGrounded;
    public static LeftWallDelegate OnWall;
    private LayerMask GroundLayer;

    [SerializeField] private bool OnGround, OnWallLeft, OnWallRight;

    private readonly Vector2 BottomOffset = Vector2.down * ColliderDistance;
    private readonly float CollisionRadius = 0.1f;
    private readonly Vector2 LeftOffset = Vector2.left * ColliderDistance;
    private readonly Vector2 RightOffset = Vector2.right * ColliderDistance;

    private void Awake()
    {
        GroundLayer = LayerMask.GetMask("Ground");
    }
    // Update is called once per frame
    private void Update()
    {
        var prevGrounded = OnGround;
        OnGround = Physics2D.OverlapBox((Vector2)transform.position + BottomOffset, new Vector2(0.7f, 0.2f), 0,
            GroundLayer);

        //Only invoke the event if the grounded state has changed
        if (OnGround != prevGrounded)
            OnGrounded?.Invoke(OnGround);

        OnWallLeft = Physics2D.OverlapCircle((Vector2)transform.position + LeftOffset, CollisionRadius, GroundLayer);
        OnWallRight = Physics2D.OverlapCircle((Vector2)transform.position + RightOffset, CollisionRadius, GroundLayer);

        if (OnWallLeft)
            OnWall?.Invoke(Vector2.left);
        else if (OnWallRight)
            OnWall?.Invoke(Vector2.right);
        else
            OnWall?.Invoke(Vector2.zero);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube((Vector2)transform.position + BottomOffset, new Vector2(0.7f, 0.2f));
        Gizmos.DrawWireSphere((Vector2)transform.position + LeftOffset, CollisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + RightOffset, CollisionRadius);

    }
}