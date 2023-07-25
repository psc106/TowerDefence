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

            //forward 기준 왼쪽에 있으면 y회전 -3
            //forward 기준 오른쪽에 있으면 y회전 +3
            //만약 10도 이내로 남아있으면 바로 목표 고정

            //바라봐야 하는 벡터를 구한다.
            //두 벡터의 차(원점, 목표)로 상대 좌표를 구한다는 뜻
            Vector3 direction = (targetPosition - position);
            //목표 각도 구한다.
            float angle = Vector3.Angle(transform.forward, direction);

            //나중에 추가할것
            //추적 각도
            //추적전 회전속도 radian 
            //추적시 회전속도 speed
            
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



            //보간 회전
            //문제점 가까워 질수록 속도가 준다.
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
