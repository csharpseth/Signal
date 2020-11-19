using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAudio : MonoBehaviour
{
    private AudioSource src;

    public float pitchVariation = 0.1f;
    public float minVolume = 0.3f;
    public float maxVolume = 0.8f;

    public float minTimeToPlay = 15f;
    public float maxTimeToPlay = 60f;

    public float minTimeToPause = 5f;
    public float maxTimeToPause = 15f;

    private float timeToPlay = 0f;
    private float timeToPause = 0f;

    private float time = 0f;

    private bool paused = false;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
        
    }

    private void Randomize()
    {
        timeToPlay = Random.Range(minTimeToPlay, maxTimeToPlay);
        timeToPause = Random.Range(minTimeToPause, maxTimeToPause);
        src.time = Random.Range(0f, src.clip.length);
        src.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        src.volume = Random.Range(minVolume, maxVolume);
        time = 0f;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(paused)
        {
            if(time >= timeToPause)
            {
                paused = false;
                Randomize();
                src.Play();
            }
        }else
        {
            if(time >= timeToPlay)
            {
                time = 0f;
                paused = true;
                src.Stop();
            }
        }

    }
}
