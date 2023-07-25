using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class OptionUI : MonoBehaviour
{
    public Button sellBtn;
    public Button downBtn;
    public List<Button> upgradeBtns;

    public GameObject cannonUI;

    public bool buttonDown;
    private bool enter;

    [SerializeField]
    private TowerCommon selectCannon;


    private void Start()
    {
        cannonUI.SetActive(false);
        buttonDown = false;
        enter = false;
    }


    //�ش� ĳ�� onclick�̺�Ʈ ���Խ�Ű��
    //���׷��̵� �ÿ��� �ش� ĳ���� ���� �ҷ�����
    //�ٿ�׷��̵� �ÿ��� �ش� ĳ�� ����� down ĳ�� ���� �ҷ�����

    //�� Ŭ���������� ���׷��̵�->�ش� ĳ���� �޾ƿ� ���׷��̵� ĳ���� ��Ŭ�� ���׷��̵� ������ �ִ´�.
    //�� Ŭ���������� �ٿ�׷��̵��->�ش� ĳ���� ��Ŭ�� �ٿ�׷��̵� ������ �ִ´�.

    private void Update()
    {
        if (GameManager.Instance.isCreateState)
        {
            cannonUI.SetActive(false);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.direction = Vector3.down;
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (!buttonDown)
            {
                enter = false;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag.Equals("Cannon"))
                    {
                        TowerCommon collider = hit.collider.transform.GetComponentInParent<TowerCommon>();

                        if (collider.isActive)
                        {
                            enter = true;
                            selectCannon = collider;
                            cannonUI.SetActive(true);
                            GameManager.Instance.pool.OpenViewRanges();

                            cannonUI.transform.position = new Vector3(hit.collider.transform.position.x, transform.position.y, hit.collider.transform.position.z);

                            if (selectCannon.GetDowngrade() == null) 
                            {
                                downBtn.gameObject.SetActive(false);
                            }
                            else
                            { 
                                downBtn.gameObject.SetActive(true);
                            }

                            List<GameObject> list = selectCannon.GetUpgrade();
                            int count = 0;
                            if (list != null)
                            {
                                count+=list.Count;

                                for (int i = 0; i < list.Count; i++)
                                {
                                    upgradeBtns[i].GetComponent<UpgradeButton>().upCannon = list[i];
                                    upgradeBtns[i].gameObject.SetActive(true);
                                }
                            }
                            for (int i = count; i < 3 ; i++)
                            {
                                upgradeBtns[i].gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(!buttonDown && !enter)
            {
                if (selectCannon != null)
                {
                    List<GameObject> list = selectCannon.GetUpgrade();
                    cannonUI.SetActive(false);
                    GameManager.Instance.pool.CloseViewRanges();
                    if (list != null)
                    {

                        for (int i = 0; i < list.Count; i++)
                        {
                            upgradeBtns[i].gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
        
    }

    public void UpgradeCannon(GameObject upgradeCannon)
    {
        if (upgradeCannon != null)
        {
            buttonDown = false;
            Node tmp = selectCannon.node;
            GameManager.Instance.pool.RemoveTower(selectCannon);
            Destroy(selectCannon.gameObject);
            upgradeCannon = Instantiate(upgradeCannon, selectCannon.transform.position, selectCannon.transform.rotation, GameManager.Instance.pool.transform);
            selectCannon = upgradeCannon.GetComponent<TowerCommon>();
            selectCannon.node = tmp;
            selectCannon.isActive = true;

            if (selectCannon.GetDowngrade() == null)
            {
                downBtn.gameObject.SetActive(false);
            }
            else
            {
                downBtn.gameObject.SetActive(true);
            }

            List<GameObject> list = selectCannon.GetUpgrade();
            int count = 0;
            if (list != null)
            {
                count += list.Count;

                for (int i = 0; i < list.Count; i++)
                {
                    upgradeBtns[i].GetComponent<UpgradeButton>().upCannon = list[i];
                    upgradeBtns[i].gameObject.SetActive(true);
                }
            }
            for (int i = count; i < 3; i++)
            {
                upgradeBtns[i].gameObject.SetActive(false);
            }
        }
    }

    public void DowngradeCannon()
    {
        GameObject downCannon = selectCannon.GetDowngrade();

        if (downCannon != null)
        {
            Node tmp = selectCannon.node;
            GameManager.Instance.pool.RemoveTower(selectCannon);
            Destroy(selectCannon.gameObject);
            downCannon = Instantiate(downCannon, selectCannon.transform.position, selectCannon.transform.rotation, GameManager.Instance.pool.transform);
            selectCannon = downCannon.GetComponent<TowerCommon>();
            selectCannon.node = tmp;
            selectCannon.isActive = true;

            if (selectCannon.GetDowngrade() == null)
            {
                downBtn.gameObject.SetActive(false);
            }
            else
            {
                downBtn.gameObject.SetActive(true);
            }

            List<GameObject> list = selectCannon.GetUpgrade();
            int count = 0;
            if (list != null)
            {
                count += list.Count;

                for (int i = 0; i < list.Count; i++)
                {
                    upgradeBtns[i].GetComponent<UpgradeButton>().upCannon = list[i];
                    upgradeBtns[i].gameObject.SetActive(true);
                }
            }
            for (int i = count; i < 3; i++)
            {
                upgradeBtns[i].gameObject.SetActive(false);
            }
        }
    }

    public void SellCannon()
    {
        selectCannon.Sell();
        cannonUI.SetActive(false);
        GameManager.Instance.pool.OpenViewRanges();
    }

}