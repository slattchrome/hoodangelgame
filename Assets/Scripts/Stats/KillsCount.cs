using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillsCount : MonoBehaviour
{
    [SerializeField]
    private StatsHandler statsHandler;

    private TextMeshProUGUI numberOfKills;

    private void Start()
    {
        numberOfKills = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (statsHandler != null)
        {
            numberOfKills.text = statsHandler.killsCount.ToString();
        }
    }
}
