using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public float speed;

    public float poison;
    public float ice;

    [SerializeField]
    private Vector3 start;
    [SerializeField]
    private Vector3 end;
    [SerializeField]
    private bool isMove;
    public bool isReturn;

    [SerializeField]
    bool[] warningArea;

    Node[][] nodes;


    // Start is called before the first frame update
    void Start()
    {
       

        speed = 40;

        warningArea = new bool[4];
        for (int i = 0; i < warningArea.Length; i++) { warningArea[i] = false; }

        hp = SetHpToLevel();
        speed = SetSpeedToLevel();

        isMove = false;
        isReturn = false;

        nodes = GameManager.Instance.nodes;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMove)
        {
            start = transform.position;
            int rand = Random.Range(-6, 11);
            float x; float z;

            if (!isReturn)
            {
                //¦��(����)
                if (rand%2 == 0)
                {
                    if (rand >= 0)
                    {
                        //�״��
                        z = (int)(transform.position.z + 10) - 10 + .5f;
                        //��
                        if (rand == 10)
                        {
                            x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                        }
                        //��
                        else
                        {
                            x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                        }
                    }
                    else
                    {
                        //����
                        if (rand == -6)
                        {
                            z = (int)(transform.position.z + 10) - 10 + .5f - 1;
                            x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                        }
                        //�Ͽ�
                        else
                        {
                            z = (int)(transform.position.z + 10) - 10 + .5f - 1;
                            x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                        }
                    }
                }
                //Ȧ��(����)
                else
                {
                    if (rand >= 0)
                    {
                        //�׷���
                        x = (int)(transform.position.x + 10) - 10 + .5f;
                        //��
                        if (rand == 9)
                        {
                            z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                        }
                        //��
                        else
                        {
                            z = (int)(transform.position.z + 10) - 10 + .5f - 1;
                        }
                    }
                    else
                    {
                        //����
                        if (rand == -5)
                        {
                            z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                            x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                        }
                        //���
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
                //¦��(����)
                if (rand % 2 == 0)
                {
                    if (rand >= 0)
                    {
                        //�״��
                        z = (int)(transform.position.z + 10) - 10 + .5f;
                        //��
                        if (rand == 10)
                        {
                            x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                        }
                        //��
                        else
                        {
                            x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                        }
                    }
                    else
                    {
                        //���
                        if (rand > -6)
                        {
                            z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                            x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                        }
                        //����
                        else
                        {
                            z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                            x = (int)(transform.position.x + 10) - 10 + .5f - 1;
                        }
                    }
                }
                //Ȧ��(����)
                else
                {
                    if (rand >= 0)
                    {
                        //�׷���
                        x = (int)(transform.position.x + 10) - 10 + .5f;
                        //��
                        if (rand ==9)
                        {
                            z = (int)(transform.position.z + 10) - 10 + .5f - 1;
                        }
                        //��
                        else
                        {
                            z = (int)(transform.position.z + 10) - 10 + .5f + 1;
                        }
                    }
                    else
                    {
                        //�Ͽ�
                        if (rand == -5)
                        {
                            z = (int)(transform.position.z + 10) - 10 + .5f -1;
                            x = (int)(transform.position.x + 10) - 10 + .5f + 1;
                        }
                        //����
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
        return 30; //Mathf.Log(GameManager.Instance.stageLevel * 2);
    }

}
