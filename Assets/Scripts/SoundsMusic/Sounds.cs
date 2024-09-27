using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sounds : MonoBehaviour
{
    [SerializeField] private float[] _soundLevels;
    [SerializeField] private AudioClip[] _sounds;
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(int soundIndex)
    {
        _audioSource.clip = _sounds[soundIndex];
        _audioSource.volume = _soundLevels[soundIndex];
        _audioSource.Play();
    }
}
