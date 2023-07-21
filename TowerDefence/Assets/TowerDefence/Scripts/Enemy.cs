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

    Rigidbody rigidbody;
    Transform image;


    // Start is called before the first frame update
    void Start()
    {
        speed = 4;

        rigidbody = GetComponentInChildren<Rigidbody>();
        image = rigidbody.transform;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));

        warningArea = new bool[4];
        for (int i = 0; i < warningArea.Length; i++) { warningArea[i] = false; }

        hp = SetHpToLevel();
        speed = SetSpeedToLevel();

        isMove = false;
        isReturn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMove)
        {
            if (!isReturn)
            {
                start = transform.position;
                end = GameManager.Instance.end.transform.position;
            }
            else
            {
                start = transform.position;
                end = GameManager.Instance.spawner.transform.position;

            }
            StartCoroutine(move());
        }
    }

    IEnumerator move()
    {
        float cnt = 0;
        isMove = true;

        while (true)
        {
            cnt += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, cnt);

            yield return new WaitForSeconds(cnt/speed);

            if(cnt >= 1) 
            {
                break;            
            }
            
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
