using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerLv1 : TowerCommon
{
    public BulletCommon bullet;

    private bool isTargeting = false;
    private GameObject target =default;
    private float FireReload = default;

    protected override void Init()
    {
        base.Init();

        isTargeting = false;
        target = default;
        FireReload = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        FireReload += Time.deltaTime;

        if (isTargeting)
        {
            if (FireReload >= fireRate)
            {
                if (target != null && target != default)
                {
                    FireReload = 0;
                    BulletCommon bullet_ = Instantiate(bullet, new Vector3(transform.position.x, .15f, transform.position.z), Quaternion.identity);
                    bullet_.transform.LookAt(new Vector3(transform.forward.x, bullet.transform.position.y, transform.forward.z));
                    bullet_.setTargetPosition(transform.forward);
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            if (target == null || target == default)
            {
                isTargeting = true;
                target = other.gameObject;
            }

            Vector3 position = new Vector3 (transform.position.x, 0, transform.position.z);
            Vector3 targetPosition = new Vector3(target.transform.position.x, 0, target.transform.position.z);
            Vector3 otherPosition = new Vector3(other.transform.position.x, 0, other.transform.position.z);

            if (Vector3.Distance(transform.position, targetPosition)>Vector3.Distance(transform.position, otherPosition))
            {
                target = other.gameObject;
            }

            //forward ���� ���ʿ� ������ yȸ�� -3
            //forward ���� �����ʿ� ������ yȸ�� +3
            //���� 10�� �̳��� ���������� �ٷ� ��ǥ ����

            //�ٶ���� �ϴ� ���͸� ���Ѵ�.
            //�� ������ ��(����, ��ǥ)�� ��� ��ǥ�� ���Ѵٴ� ��
            Vector3 direction = (targetPosition - position);
            //��ǥ ���� ���Ѵ�.
            float angle = Vector3.Angle(transform.forward, direction);

            //���߿� �߰��Ұ�
            //���� ����
            //������ ȸ���ӵ� radian 
            //������ ȸ���ӵ� speed
            
            if (angle > 30f)
            {
                if (Quaternion.FromToRotation(transform.forward, direction).eulerAngles.y>180)
                {
                    transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y - 3f, 0f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y + 3f, 0f);
                }
            }
            else
            {
                //transform.LookAt(target.transform, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), 10*Time.deltaTime);
            }



            //���� ȸ��
            //������ ����� ������ �ӵ��� �ش�.
            //transform.rotation = Quaternion.Slerp(transform.rotation.normalized, Quaternion.LookRotation(new Vector3(targetPosition.x, 0, targetPosition.z)).normalized, .05F);
            //transform.rotation = Quaternion.LookRotation(new Vector3(targetPosition.x, 0, targetPosition.z)).normalized;

            //transform.LookAt(new Vector3(targetPosition.x, 0, targetPosition.z));
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (target != null && target != default)
        {
            if (other.tag.Equals("Enemy") && other.name.Equals(target.name))
            {
                isTargeting = false;
                target = null;
            }
        }
        
    }
}
