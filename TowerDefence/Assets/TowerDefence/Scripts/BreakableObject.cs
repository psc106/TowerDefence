using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    Enemy parent;

    private void Start()
    {
        parent = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("에너미 콜라이더" + other.tag.ToString());
        if (other.tag.Equals("Bullet"))
        {
            Destroy(other.gameObject);
            BulletCommon bullet = other.GetComponent<BulletCommon>();

            parent.hp -= bullet.power;
            if (parent.hp <= 0)
            {
                Destroy(parent.gameObject);
                GameManager.Instance.spawner.removeCount();
            }
        }

        if (!parent.isReturn)
        {
            if (other.tag.Equals("End"))
            {
                parent.isReturn = true;
            }
        }

        else
        {
            if (other.tag.Equals("Spawner"))
            {
                parent.isReturn = false;
            }
        }
    }
}
