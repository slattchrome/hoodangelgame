using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehavior : MonoBehaviour
{
    [SerializeField]
    private AudioClip checkpointSound;

    private Animator animator;
    private bool isReached = false;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isReached)
        {
            isReached = true;
            animator.SetBool("isReached", true);
            SoundFXManager.instance.PlaySoundFXClip(checkpointSound, transform, 1f);
        }
    }
}
