using UnityEngine;

public class PickUps : MonoBehaviour
{
    public delegate void PickUpDelegate(PickUpType Type, int value);

    public enum PickUpType
    {
        
        Jump,
        GoldCoin,
        SilverCoin,
        BronzeCoin
    }

    public static PickUpDelegate OnPickUp;

    public PickUpType Type;
    private int Value;

    // Start is called before the first frame update
    private void Start()
    {
        switch (Type)
        {
            case PickUpType.GoldCoin:
                Value = 10;
                break;
            case PickUpType.SilverCoin:
                Value = 5;
                break;
            case PickUpType.BronzeCoin:
                Value = 1;
                break;
            default:
                Value = 0;
                break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //moves up and down
        transform.position =
            new Vector2(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 2) * 0.0005f);
    }

    // if the player collides with the potion, the player gets an effect
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnPickUp?.Invoke(Type, Value);
            Destroy(gameObject);
        }
    }
}