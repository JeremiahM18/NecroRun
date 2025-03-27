using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.volume = 1.0f;
        audioSource.Play();
    }
}
