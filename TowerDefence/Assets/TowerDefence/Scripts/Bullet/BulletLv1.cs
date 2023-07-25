using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLv1 : BulletCommon
{

    protected override void Init()
    {
        base.Init();
    }

    private void Awake()
    {
        Init();
        Destroy(gameObject, 1f);

    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector3(targetPosition.x*speed, 0, targetPosition.z*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
