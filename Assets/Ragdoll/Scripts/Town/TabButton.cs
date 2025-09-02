using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image background;

    private float idleXPos;

    private void Awake()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    private void Start()
    {


        idleXPos = transform.localPosition.x;
        ResetXPos();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
         tabGroup.OnTabSelected(this);
         AudioManager.Instance.PlayEffect("Clicked");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
         tabGroup.OnTabEnter(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    public void ResetXPos()
    {
        LeanTween.moveLocalX(gameObject, idleXPos, 0.2f);
    }

    public void SelectedXPos()
    {
        LeanTween.moveLocalX(gameObject, idleXPos + 50, 0.2f);
    }

    
}
