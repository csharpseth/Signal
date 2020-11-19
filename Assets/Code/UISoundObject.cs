using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundObject : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalSoundManager.PlaySound(clickSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GlobalSoundManager.PlaySound(hoverSound);
    }
}
