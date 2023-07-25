using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public float speed;

    public float poison;
    public float ice;

    private Vector3 start;
    private Vector3 end;

    public bool isReturn;
    private bool isMove;
    private bool isRun;
    private bool isDead;

    [SerializeField]
    bool[] warningArea;

    Node[][] nodes;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

         speed = 40;

        warningArea = new bool[4];
        for (int i = 0; i < warningArea.Length; i++) { warningArea[i] = false; }

        hp = SetHpToLevel();
        speed = SetSpeedToLevel();

        isMove = false;
        isReturn = false;
        isRun = false;
        isDead = false;

        nodes = GameManager.Instance.nodes;
        animator.SetBool("Walk Forward", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (!isMove)
            {
                start = transform.position;
                int rand = Random.Range(-6, 11);
                float x; float z;

                if (!isReturn)
                {
                    //짝수(가로)
                    if (rand % 2 == 0)
                    {
                        if (rand >= 0)
                        {
                            //그대로
                            z = (int)(transform.position.z + 10) - 10 + .5f;
                            //좌
                            if (rand == 10)
                            {
                                x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                            }
                            //우
                            else
                            {
                                x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                            }
                        }
                        else
                        {
                            //하좌
                            if (rand == -6)
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f - 1;
                                x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                            }
                            //하우
                            else
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f - 1;
                                x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                            }
                        }
                    }
                    //홀수(세로)
                    else
                    {
                        if (rand >= 0)
                        {
                            //그래도
                            x = (int)(transform.position.x + 10) - 10 + .5f;
                            //상
                            if (rand == 9)
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                            }
                            //하
                            else
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f - 1;
                            }
                        }
                        else
                        {
                            //상좌
                            if (rand == -5)
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                                x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                            }
                            //상우
                            else
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                                x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                            }
                        }

                    }

                }
                else
                {
                    //짝수(가로)
                    if (rand % 2 == 0)
                    {
                        if (rand >= 0)
                        {
                            //그대로
                            z = (int)(transform.position.z + 10) - 10 + .5f;
                            //우
                            if (rand == 10)
                            {
                                x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                            }
                            //좌
                            else
                            {
                                x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                            }
                        }
                        else
                        {
                            //상우
                            if (rand > -6)
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                                x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                            }
                            //상좌
                            else
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                                x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                            }
                        }
                    }
                    //홀수(세로)
                    else
                    {
                        if (rand >= 0)
                        {
                            //그래도
                            x = (int)(transform.position.x + 10) - 10 + .5f;
                            //하
                            if (rand == 9)
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f - 1;
                            }
                            //상
                            else
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                            }
                        }
                        else
                        {
                            //하우
                            if (rand == -5)
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f - 1;
                                x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                            }
                            //하좌
                            else
                            {
                                z = (int)(transform.position.z + 10) - 10 + .5f - 1;
                                x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                            }
                        }

                    }
                }


                if (x > 9)
                {
                    x -= 2;
                }
                else if (x < -9)
                {
                    x += 2;
                }

                if (z > 9)
                {
                    z -= 2;
                }
                else if (z < -9)
                {
                    z += 2;
                }


                end = new Vector3(x, .5f, z);
                transform.LookAt(end, Vector3.up);


                StartCoroutine(move());
            }
        }
    }

    IEnumerator move()
    {
        float cnt = 0;
        isMove = true;

        while (true)
        {
            cnt += 1;
            transform.position = Vector3.Lerp(start, end, cnt/speed);

            yield return new WaitForEndOfFrame();

            if (cnt > speed) break;
        }

        isMove = false;
    }

    Vector3 GetNextPosition(int direction)
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                break; 
            case 1:
                break;
            case 2:
                break;
            case 3:
                break; 
            default : 
                break;
        }
        return new Vector3();

    } 

    int SetHpToLevel()
    {
        return (int)Mathf.Exp( GameManager.Instance.stageLevel)+10;
    }
    float SetSpeedToLevel()
    {
        return speed; //Mathf.Log(GameManager.Instance.stageLevel * 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            //Debug.Log("에너미 콜라이더" + other.tag.ToString());
            if (other.tag.Equals("Bullet"))
            {
                Destroy(other.gameObject);
                BulletCommon bullet = other.GetComponent<BulletCommon>();

                hp -= bullet.power;
                if (hp < 5)
                {
                    if (!isRun)
                    {
                        isRun = true;
                        animator.SetBool("Walk Forward", false);
                        animator.SetBool("Run Forward", true);
                    }
                    if (speed > 4)
                    {
                        speed *= .4f;
                    }
                    else
                    {
                        speed = 2;
                    }

                    if (hp <= 0)
                    {
                        isDead = true;
                        Destroy(gameObject, 1f);

                        animator.SetTrigger("Die");
                        GameManager.Instance.spawner.removeCount();
                    }
                }
            }

            if (!isReturn)
            {
                if (other.tag.Equals("End"))
                {
                    isReturn = true;
                }
            }

            else
            {
                if (other.tag.Equals("Spawner"))
                {
                    isReturn = false;
                }
            }
        }
    }

    IEnumerator JumpRun()
    {
        while (!isDead)
        {
            animator.SetTrigger("Smash Attack");
            yield return new WaitForSeconds(.3f);
        }
    }
}
