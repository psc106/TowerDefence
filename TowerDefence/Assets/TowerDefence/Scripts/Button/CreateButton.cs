using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    TowerUI parent;

    private void Start()
    {
        parent = GetComponentInParent<TowerUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.isCreateState)
        {
            parent.CreateCannon();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.Instance.isCreateState)
        {
            parent.BuildCannon();
        }
    }

}
