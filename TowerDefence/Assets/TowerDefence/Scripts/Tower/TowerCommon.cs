using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCommon : MonoBehaviour
{
    public float fireRate;
    public int bulletCount;
    public float fireRadius;

    protected SphereCollider shootArea;
    protected Rigidbody rigidbody;

    public List<GameObject> upgradeCannons;
    public GameObject downgradeCannon;

    public bool isActive = false;
    public Node node;

    protected virtual void Init()
    {
        shootArea = GetComponent<SphereCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public List<GameObject> GetUpgrade()
    {
        return upgradeCannons;
    }


    public GameObject GetDowngrade()
    {
        return downgradeCannon;
    }

    public void Sell()
    {
        node.SetBuildPossible(false);
        node.canBuild -= 1;
        node.isCannon = false;
        node.SelectNode(false);
        Destroy(gameObject);
    }

    public void Upgrade()
    {
        if (upgradeCannons != null && upgradeCannons.Count > 0)
        {
            for (int i = 0; i < upgradeCannons.Count; i++)
            {
                upgradeCannons[i].gameObject.SetActive(false);
            }
        }
        Destroy(gameObject);
    }
}
