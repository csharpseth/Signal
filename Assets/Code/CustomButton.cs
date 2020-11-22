using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite clickSprite;

    public LoadLevel onClick;

    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
        img.sprite = normalSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        img.sprite = clickSprite;
        onClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = normalSprite;
    }
}

[System.Serializable]
public class LoadLevel : UnityEvent { }
