using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemIdentifier identification;
    public GameObject prefab;
    public AudioClip pickupSound;

    public ItemIdentifier Collect()
    {
        PlayerSoundController.instance.PlaySound(pickupSound);
        Destroy(gameObject, 0.1f);
        return identification;
    }

}