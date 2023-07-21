using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int stageLevel { get; private set; }

    public int maxWidth = 18;
    public int maxHeight = 18;

    public Spawner spawner;
    public End end;


    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            Init();
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    private void Init()
    {
        stageLevel = 0;

        spawner = FindObjectOfType<Spawner>();
        end = FindObjectOfType<End>();
    }

    public void LevelUpStage()
    {
        stageLevel += 1;
    }
}