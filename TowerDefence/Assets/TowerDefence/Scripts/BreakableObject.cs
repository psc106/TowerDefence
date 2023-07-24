using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    public Enemy enemy;

    private void Start()
    {
        enemy = transform.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("에너미 콜라이더" + other.tag.ToString());
        if (other.tag.Equals("Bullet"))
        {
            Destroy(other.gameObject);
            BulletCommon bullet = other.GetComponent<BulletCommon>();

            enemy.hp -= bullet.power;
            if (enemy.hp <= 0)
            {
                Destroy(enemy.gameObject);
                GameManager.Instance.spawner.removeCount();
            }
        }

        if (!enemy.isReturn)
        {
            if (other.tag.Equals("End"))
            {
                enemy.isReturn = true;
            }
        }

        else
        {
            if (other.tag.Equals("Spawner"))
            {
                enemy.isReturn = false;
            }
        }
    }
}
