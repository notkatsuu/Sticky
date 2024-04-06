using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInput : PlayerInput
{

    private int Value = 1;
    Collider2D Previous;
    void Start()
    {
    }
    private void Update()
    {
        HorizontalInput = Value;

        if(Random.Range(0, 10) == 1) OnJumpInput?.Invoke(true);

        if (Random.Range(0, 20) == 1) OnDashInput?.Invoke();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ChangeDirection")
        {
            Value *= -1;
        }
        
    }
}
