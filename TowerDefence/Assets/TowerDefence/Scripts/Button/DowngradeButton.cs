using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DowngradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    OptionUI parent;
    bool isObject;

    private void Start()
    {
        parent = GetComponentInParent<OptionUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isObject = true;
        parent.buttonDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isObject)
        {
            parent.buttonDown = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        parent.DowngradeCannon();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isObject = false;
        parent.buttonDown = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isObject = true;
    }
}
