using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public int x;
    public int z;

    public bool canBuild;
    public bool isCannon;

    public Sprite noBuild;
    public Sprite selection;
    public Sprite unSelection;

    private void Start()
    {
        canBuild = true;
        isCannon = false;
    }
    private void Update()
    {
        
    }

    public void SelectNode(bool isSelect)
    {
        if (!canBuild)
        {
            gameObject.GetComponent<Image>().sprite = noBuild;
        }
        else
        {
            if (isSelect)
            {
                gameObject.GetComponent<Image>().sprite = selection;
            }

            else
            {
                gameObject.GetComponent<Image>().sprite = unSelection;
            }
        }
    }
}
