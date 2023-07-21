using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCommon : MonoBehaviour
{
    public Bullet bullet;
    public float fireRate;
    public float bulletCount;
    public float fireRadius;

    protected SphereCollider shootArea;
    protected Rigidbody rigidbody;

    protected virtual void Init()
    {
        shootArea = GetComponent<SphereCollider>();
        rigidbody = GetComponent<Rigidbody>();
        bullet = GetComponent<Bullet>();
    }
}
