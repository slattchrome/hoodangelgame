using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerksCount : MonoBehaviour
{
    [SerializeField]
    private StatsHandler statsHandler;

    private TextMeshProUGUI numberOfPerks;

    private void Start()
    {
        numberOfPerks = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {   
        if (statsHandler != null)
        {
            numberOfPerks.text = statsHandler.perksCount.ToString();
        }    
    }
}
