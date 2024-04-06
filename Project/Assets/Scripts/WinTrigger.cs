using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    private void Update()
    {
        float scale = Mathf.Sin(Time.time* 0.3f)*0.5f + 1.0f;
        transform.localScale = new Vector3(scale, scale, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene("Ending");
        }
    }
}