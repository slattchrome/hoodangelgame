using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsManager : MonoBehaviour
{
    public static KillsManager instance; 

    public event Action<int> OnKillCountChanged;

    private int killsCount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        killsCount = 0;
    }

    public void IncrementKillCount()
    {
        killsCount++;
        OnKillCountChanged?.Invoke(killsCount); 
    }

    public void SetKillsCount(int killsCount)
    {
        this.killsCount = killsCount;
    }

    public int GetKillsCount()
    {
        return killsCount;
    }
}
