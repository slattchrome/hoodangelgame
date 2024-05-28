using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField]
    private AudioSource soundFXObject;

    [SerializeField]
    private AudioMixerGroup audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;    

        audioSource.Play();

        float clipLength = audioSource.clip.length; 

        Destroy(audioSource.gameObject, clipLength);

        audioSource.outputAudioMixerGroup = audioMixer;
    }
}
