using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    
    private AudioSource AudioSource;
    private float startTime;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }


    public void PlaySound(string sound)
    {
        
        AudioSource.PlayOneShot(Resources.Load<AudioClip>(sound));
        startTime = Time.time;
    }

    public bool isPlaying()
    {
        if ((Time.time - startTime) >= 0.2)
            return false;
        return true;
    }

    private void OnDestroy()
    {
        AudioSource = null;
    }
    
    
    
}