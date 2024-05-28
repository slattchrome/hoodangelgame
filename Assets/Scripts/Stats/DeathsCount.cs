using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathsCount : MonoBehaviour
{
    [SerializeField]
    private StatsHandler statsHandler;

    private TextMeshProUGUI numbersOfDeath;

    private void Start()
    {
        numbersOfDeath = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (statsHandler != null)
        {
            numbersOfDeath.text = statsHandler.deathsCount.ToString();
        }
    }
}
