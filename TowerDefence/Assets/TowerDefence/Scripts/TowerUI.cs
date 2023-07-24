using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    int[] dx = { 0, 0, 1, -1 };
    int[] dz = { 1, -1, 0, 0 };

    public GameObject cannonPrefab;

    public GameObject imageUI;
    private Node[][] nodes;
    private Node currNode;

    private GameObject currCannon;

    // Start is called before the first frame update
    void Start()
    {
        int mapsizeWidth = GameManager.Instance.maxWidth;
        this.nodes = new Node[GameManager.Instance.maxHeight][];
        for (int i = 0; i < GameManager.Instance.maxHeight; i++)
        {
            this.nodes[i] = new Node[mapsizeWidth];
        }
        Node[] images = imageUI.GetComponentsInChildren<Node>();

        foreach (Node node in images)
        {
            node.x = (int)(node.transform.position.x + 9);
            node.z = (int)(node.transform.position.z + 9);


            node.gameObject.name = "Node_" + node.x + "_" + node.z;

            this.nodes[node.x][node.z] = node;
            if ((node.x < 3 && node.z > 14) || (node.x > 14 && node.z < 3))
            {
                node.canBuild = false;
            }

            node.SelectNode(false);
        }
        imageUI.SetActive(false);
    }

    public Node[][] GetNodes()
    {
        return this.nodes;
    }

    private void Update()
    {

        if (GameManager.Instance.isCreateState)
        {
            imageUI.SetActive(true);
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            if ((point.x < 9 && point.x > -9) && (point.z < 9 && point.z > -9))
            {
                if (!currCannon.activeInHierarchy)
                {
                    currCannon.SetActive(true);
                }
                point = new Vector3(((int)(point.x + 10)) - 10 + .5f, 5f, ((int)(point.z + 10)) - 10 + .5f);

                currCannon.transform.position = point;
                if (currNode != null)
                {
                    currNode.SelectNode(false);
                }


                if (Input.GetMouseButtonUp(0))
                {
                    BuildCannon();
                }

                currNode = nodes[(int)(point.x + 10) - 10 + 9][(int)(point.z + 10) - 10 + 9];
                currNode.SelectNode(true);
            }
            else
            {
                currCannon.SetActive(false);
                if (currNode != null)
                {
                    currNode.SelectNode(false);
                    currNode = null;
                }
            }
        }
    }
    public void BuildCannon()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        point = new Vector3(((int)(point.x + 10)) - 10 + .5f, 5f, ((int)(point.z + 10)) - 10 + .5f);

        if (!((point.x < 9 && point.x > -9) && (point.z < 9 && point.z > -9))) return;

        Node currNode = nodes[(int)(point.x + 10) - 10 + 9][(int)(point.z + 10) - 10 + 9];

        if (!currNode.canBuild) return;

        for (int i = 0; i < 4; i++)
        {
            int x = (int)(point.x + 10) - 10 + 9 + dx[i];
            int z = (int)(point.z + 10) - 10 + 9 + dz[i];

            if (x < 0) continue;
            else if (x > 17) continue;
            else if (z < 0) continue;
            else if (z > 17) continue;
            Debug.Log(x + "/" + z );
            nodes[x][z].canBuild = false;
            nodes[x][z].SelectNode(false);
        }
        currCannon.transform.position = new Vector3(point.x, .5f, point.z);
        TowerCommon towerScript = currCannon.GetComponent<TowerCommon>();
        towerScript.enabled = true;

        currNode.canBuild = false;
        currNode.isCannon = false;

        Time.timeScale = 1f;
        GameManager.Instance.isCreateState = false;
        imageUI.SetActive(false);
    }

    public void CreateCannon()
    {
        if (!GameManager.Instance.isCreateState)
        {
            Time.timeScale = 0.4f;
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                Input.mousePosition.y, Input.mousePosition.z));
            point = new Vector3(point.x, 5f, point.z);

            currCannon = Instantiate(cannonPrefab, point, Quaternion.identity);

            TowerCommon towerScript = currCannon.GetComponent<TowerCommon>();
            towerScript.enabled = false;

            GameManager.Instance.isCreateState = true;
        }
    }
}
