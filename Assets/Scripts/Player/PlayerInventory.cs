using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private AudioClip collectSound;

    private int numberOfPerks;

    public UnityEvent<PlayerInventory> OnPerkCollected;

    public void PerkCollected()
    {
        SoundFXManager.instance.PlaySoundFXClip(collectSound, transform, 1f);
        numberOfPerks++;
        OnPerkCollected.Invoke(this);
    }

    public void SetNumberOfPerks(int numberOfPerks)
    {
        this.numberOfPerks = numberOfPerks;
    }

    public int GetNumberOfPerks()
    {
        return numberOfPerks;
    } 
}
