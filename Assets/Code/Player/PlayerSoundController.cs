using UnityEngine;
using System.Collections;

public class PlayerSoundController : MonoBehaviour
{
    public static PlayerSoundController instance;
    private AudioSource src;
    private void Awake()
    {
        instance = this;
        src = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        src.PlayOneShot(clip);
    }
}