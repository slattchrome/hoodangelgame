using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField]
    private Transform vfxHitGreen;
    [SerializeField]
    private Transform vfxHitRed;
    [SerializeField]
    private AudioClip enemyDeathSound;

    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 80f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("TestTarget"))
        {
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            if (other.gameObject.CompareTag("Enemy"))
            {
                SoundFXManager.instance.PlaySoundFXClip(enemyDeathSound, transform, 1f);
                Destroy(other.gameObject);
                KillsManager.instance.IncrementKillCount();
            }
            
        }
        else
        {
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
