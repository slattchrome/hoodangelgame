using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StatsHandler : MonoBehaviour
{
    //private const string SAVE_SEPARATOR = "##SAVE-VALUE";

    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private PlayerMovement playerMovement; 

    public int perksCount { get; private set; }

    public int killsCount { get; private set; }

    public int deathsCount { get; private set; }

    private void Start()
    {
        if (PlayerPrefs.HasKey("perksCount") && PlayerPrefs.HasKey("killsCount") && PlayerPrefs.HasKey("deathsCount"))
        {
            Load();
        }
        else
        {
            Save();
        }
    }

    private void Update()
    {
        perksCount = playerInventory.GetNumberOfPerks();
        killsCount = KillsManager.instance.GetKillsCount();
        deathsCount = playerMovement.GetDeathsCount();
        Save();
    }

    private void Save()
    {

        PlayerPrefs.SetInt("perksCount", perksCount);
        PlayerPrefs.SetInt("killsCount", killsCount);
        PlayerPrefs.SetInt("deathsCount", deathsCount);

        //string[] stats = new string[]
        //{
        //    ""+perksCount,
        //    ""+killsCount,
        //    ""+deathsCount
        //};

        //string saveString = string.Join(SAVE_SEPARATOR, stats);
        //File.WriteAllText(Application.dataPath + "/SavedStats/save.txt", saveString);
    }

    private void Load()
    {
        playerInventory.SetNumberOfPerks(PlayerPrefs.GetInt("perksCount"));
        KillsManager.instance.SetKillsCount(PlayerPrefs.GetInt("killsCount"));
        playerMovement.SetDeathsCount(PlayerPrefs.GetInt("deathsCount"));

        //string saveString = File.ReadAllText(Application.dataPath + "/SavedStats/save.txt");

        //string[] stats = saveString.Split(new[] { SAVE_SEPARATOR }, System.StringSplitOptions.None);

        //playerInventory.SetNumberOfPerks(int.Parse(stats[0]));
        //KillsManager.instance.SetKillsCount(int.Parse(stats[1]));
        //playerMovement.SetDeathsCount(int.Parse(stats[2]));
    }
}
