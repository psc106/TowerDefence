using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy = default;
    public int maxCount = default;

    private float spawnTime = default;
    private float currCount = default;

    private void Start()
    {
        currCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime >= 1)
        {
            if(currCount < maxCount)
            {
                spawnTime = 0;
                currCount += 1;

                GameObject enemy_ = Instantiate(enemy, transform.position, Quaternion.identity);
            }
        }        
    }

    public void removeCount()
    {
        currCount -= 1;
    }
}
