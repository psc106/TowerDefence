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

    protected virtual void Init()
    {
        shootArea = GetComponent<SphereCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }
}
