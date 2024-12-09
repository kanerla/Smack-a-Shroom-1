using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource negativeAudio;
    public AudioSource positiveAudio;
    public static AudioScript instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
