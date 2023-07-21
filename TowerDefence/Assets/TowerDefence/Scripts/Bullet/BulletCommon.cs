using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCommon : MonoBehaviour
{    
    protected Rigidbody rb;
    protected Vector3 targetPosition;

    public float power;
    public float speed;
    public float splashArea;

    protected virtual void Init()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void setTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }
}
