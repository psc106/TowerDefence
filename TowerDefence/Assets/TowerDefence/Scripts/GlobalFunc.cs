using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFunc
{

    public static void SetBuildPossible(this Node node, bool build)
    {
        int[] dx = { 0, 0, 1, -1, -1, 1, 1, -1 };
        int[] dz = { 1, -1, 0, 0, -1, 1, -1, 1 };

        for (int i = 0; i < 8; i++)
        {
            int x = node.x + dx[i];
            int z = node.z + dz[i];

            if (x < 0) continue;
            else if (x > 17) continue;
            else if (z < 0) continue;
            else if (z > 17) continue;
            if (build)
            {
                GameManager.Instance.nodes[x][z].canBuild += 1;
            }
            if (!build)
            {
                GameManager.Instance.nodes[x][z].canBuild -= 1;
            }
            GameManager.Instance.nodes[x][z].SelectNode(false);
        }
    }
}
