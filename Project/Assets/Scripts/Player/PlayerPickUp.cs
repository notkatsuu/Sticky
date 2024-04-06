using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{

    private PlayerController PlayerController;

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        PickUps.OnPickUp += OnPickUp;
    }
    private void OnDestroy()
    {
        PickUps.OnPickUp -= OnPickUp;
    }

    void OnPickUp(PickUps.PickUpType type, int points)
    {
        
        switch (type)
        {
            case PickUps.PickUpType.Jump:
                PlayerController.GetComponent<Jump>().IncreaseMaxUses(1);

                PlayerController.AudioController.PlaySound(PlayerSounds.PICKUP);
                break;
            case PickUps.PickUpType.GoldCoin:
            case PickUps.PickUpType.SilverCoin:
            case PickUps.PickUpType.BronzeCoin:
                // add points
                PlayerController.AudioController.PlaySound(PlayerSounds.COIN);
                Debug.Log($"Added {points} points");
                break;
        }
    }
}
