using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void DashInputDelegate();
    public delegate void JumpInputDelegate(bool isJumping);
    public delegate void MovementInputDelegate();
    public delegate void ResetSceneInputDelegate();

    public static MovementInputDelegate OnMovementInput;
    public static JumpInputDelegate OnJumpInput;
    public static DashInputDelegate OnDashInput;
    public static ResetSceneInputDelegate OnResetSceneInput;


    public float HorizontalInput { get; protected set; }
    public float VerticalInput { get; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
            HorizontalInput = Input.GetAxis("Horizontal");
        else
            HorizontalInput = 0;

        if (Input.GetKeyDown(KeyCode.Space)) OnJumpInput?.Invoke(true);
        if (Input.GetKeyUp(KeyCode.Space)) OnJumpInput?.Invoke(false);

        if (Input.GetKeyDown(KeyCode.LeftShift)) OnDashInput?.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindAnyObjectByType<Score>().ResetScore();
            FindAnyObjectByType<SceneController>()?.LoadScene("Title");
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            FindAnyObjectByType<Score>()?.ResetScore();
            OnResetSceneInput?.Invoke();
            
        }
    }
}