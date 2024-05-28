using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoyUI : MonoBehaviour
{

    private TextMeshProUGUI perkText;

    [SerializeField]
    private PlayerInventory playerInventory;

    void Start()
    {
        perkText = GetComponent<TextMeshProUGUI>();
        UpdatePerkText(playerInventory);
    }

    public void UpdatePerkText(PlayerInventory playerInventory)
    {
        perkText.text = playerInventory.GetNumberOfPerks().ToString();
    }
}
