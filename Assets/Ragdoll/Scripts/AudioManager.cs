using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public SoundEffect[] soundEffects;

    public GameObject AudioSourcePrefab;
    public AudioSource audioSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEffect(string name)
    {
        SoundEffect sound = Array.Find(soundEffects, x => x.name == name);

        if (sound == null)
        {
            print("Sound not found");   
        }
        else
        {
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume / 100f;
            audioSource.pitch = Time.timeScale;
            audioSource.spatialBlend = 0;

            audioSource.Play();

            var obj = Instantiate(AudioSourcePrefab, transform.position, Quaternion.identity);
            Destroy(obj.gameObject, sound.clip.length);
            
        }
    }

    public void PlayEffect3DSound(string name, float minDist, float maxDist, Vector2 pos)
    {
        SoundEffect sound = Array.Find(soundEffects, x => x.name == name);

        if (sound == null)
        {
            print("Sound not found");   
        }
        else
        {
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume / 100f;
            audioSource.pitch = Time.timeScale;
            audioSource.minDistance = minDist;
            audioSource.maxDistance = maxDist;
            audioSource.spatialBlend = 1;
            audioSource.Play();

            var obj = Instantiate(AudioSourcePrefab, pos, Quaternion.identity);
            Destroy(obj.gameObject, sound.clip.length);
            
        }
    }
    
    public void PlayDeathClip(string name)
    {
        SoundEffect sound = Array.Find(soundEffects, x => x.name == name);

        if (sound == null)
        {
            print("Sound not found");   
        }
        else
        {
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume / 100f;
            audioSource.pitch = 0.5f;
            audioSource.Play();

            var obj = Instantiate(AudioSourcePrefab, transform.position, Quaternion.identity);
            Destroy(obj.gameObject, sound.clip.length);
            
        }
    }

    public void PlayClipOnTime(string name, float clipLength)
    {
        SoundEffect sound = Array.Find(soundEffects, x => x.name == name);

        if (sound == null)
        {
            print("Sound not found");   
        }
        else
        {
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume / 100f;
            audioSource.pitch = 0.5f;
            audioSource.Play();

            var obj = Instantiate(AudioSourcePrefab, transform.position, Quaternion.identity);
            Destroy(obj.gameObject, clipLength);
            
        }
    }
}
