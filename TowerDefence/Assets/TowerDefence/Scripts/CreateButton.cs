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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            
        }

        parent.CreateCannon();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        parent.BuildCannon();
    }
}
