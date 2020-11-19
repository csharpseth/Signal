using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlobalSoundManager : MonoBehaviour
{
    private static GlobalSoundManager instance;
    private AudioSource src;

    private void Awake()
    {
        instance = this;
        src = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public static void PlaySound(AudioClip clip)
    {
        instance.src.PlayOneShot(clip);
    }
}
