using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] private float _volumeToAdd = 0.2f;
    [SerializeField] private float _volumeDown = 0.5f;

    float _volumeTimer = 0;
    private AudioSource _audioSource;
    VolumeSync _volumeSync;

    private void Awake()
    {
        _volumeSync = GetComponent<VolumeSync>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Particule"))
        {   
            _audioSource.volume += _volumeToAdd;
            _volumeSync.VolumeCheck();
            _volumeTimer = Time.time + _volumeDown;
        }
    }

    private void Update()
    {
        if (Time.time >= _volumeTimer && _audioSource.volume != 0)
        {
            _audioSource.volume -= _volumeToAdd;
            _volumeSync.VolumeCheck();
            _volumeTimer = Time.time + _volumeDown;
        }
    }
}
