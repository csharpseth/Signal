using UnityEngine;

public class ActiveInRange : MonoBehaviour
{
    public float range = 20f;

    private float actualRange => range * range;
    private AudioSource src;
    private RandomizeAudio rand;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
        rand = GetComponent<RandomizeAudio>();
    }

    private void LateUpdate()
    {
        if((MovementController.position - transform.position).sqrMagnitude <= actualRange)
        {
            rand.active = true;
        }else
        {
            src.Stop();
            rand.active = false;
        }
    }
}
