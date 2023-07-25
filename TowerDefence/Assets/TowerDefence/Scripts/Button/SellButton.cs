using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    OptionUI parent;

    private void Start()
    {
        parent = GetComponentInParent<OptionUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        parent.buttonDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        parent.buttonDown = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        parent.SellCannon();
        parent.buttonDown = false;
    }

}
