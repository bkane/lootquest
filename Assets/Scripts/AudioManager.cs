using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Click;

    public void PlayClick()
    {
        Source.PlayOneShot(Click);
    }
}
