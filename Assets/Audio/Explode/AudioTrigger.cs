using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        audioSource.PlayOneShot(audioClips[randomIndex]);
    }
}
