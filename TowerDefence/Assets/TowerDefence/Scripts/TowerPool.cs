using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPool : MonoBehaviour
{
    List<TowerCommon> towers;

    private void Awake()
    {
        towers = new List<TowerCommon>();
    }

    public void AddTower(TowerCommon tower)
    {
        towers.Add(tower);
    }

    public void RemoveTower(TowerCommon tower)
    {
        towers.Remove(tower);
    }

    public void OpenViewRanges()
    {
        foreach (var tower in towers)
        {
            tower.OpenViewRange();
        }

    }

    public void CloseViewRanges()
    {
        foreach (var tower in towers)
        {
            tower.CloseViewRange();
        }

    }
}
