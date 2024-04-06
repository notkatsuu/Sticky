using UnityEngine;

public abstract class Mechanic : MonoBehaviour
{
    protected int MaxUses;
    protected int UsesLeft;
    protected ActorController Controller = null;

    protected void Awake()
    {
        UsesLeft = MaxUses;
        if (Controller == null)
            Controller = GetComponent<ActorController>();
    }
    public void Call(bool use)
    {
        if (use)
        {
            StartCast();
        }
        else
        {
            StopCast();
        }
    }
    public void SetController(ActorController controller)
    {
        Controller = controller;
    }
    virtual protected void StartCast()
    {
        return;
    }
    virtual protected void StopCast()
    {
        return;
    }

    public void IncreaseMaxUses(int value)
    {
        MaxUses+=value;
    }

    public void ResetUses()
    {
        UsesLeft = MaxUses;
    }

    virtual protected void PlayAnimation(string animation)
    {
        return;
    }
}
