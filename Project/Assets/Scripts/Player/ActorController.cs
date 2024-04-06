using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ActorController : MonoBehaviour
{
    // Base class for all entity controllers that can have mechanics

    public Rigidbody2D Rigidbody { get; protected set; }
    public AudioController AudioController { get; protected set; }
    public List<Mechanic> AvailableMechanics { get; protected set; }

    public bool IsGrounded;
    public bool IsJumping;
    public bool IsMoving;
    public bool IsDashing;
    public bool IsWallJumping;
    public Vector2 WallDirection;


    protected void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        AudioController = FindObjectOfType<AudioController>();
        AvailableMechanics = new List<Mechanic>(GetComponents<Mechanic>());

        foreach (Mechanic mechanic in AvailableMechanics)
        {
            mechanic.SetController(this);
        }

    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        foreach (Mechanic mechanic in AvailableMechanics)
        {
            mechanic.SetController(null);
        }
        Rigidbody = null;
        AudioController = null;
        AvailableMechanics = null;
    }
}
