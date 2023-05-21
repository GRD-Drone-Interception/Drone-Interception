using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        int randomIndex = Random.Range(0, audioClips.Length);
        _audioSource.clip = audioClips[randomIndex];
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Defender"))
        {
            _audioSource.Play();
            //_audioSource.PlayOneShot(audioClips[randomIndex]);
        }
    }
}
