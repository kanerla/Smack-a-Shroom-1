using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource negativeAudio;
    public AudioSource positiveAudio;
    public static AudioScript instance;

    public void PlayPositive()
    {
        positiveAudio.Play();
    }

    public void PlayNegative()
    {
        negativeAudio.Play();
    }

    private void Awake()
    {
        instance = this;
    }
}
