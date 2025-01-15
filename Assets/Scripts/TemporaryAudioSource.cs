using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryAudioSource : MonoBehaviour
{

    AudioSource audioSource;

    public void setClipAndPlay(AudioClip clip)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1;
        audioSource.maxDistance = 15;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void SetClipAndPlay2D(AudioClip clip)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0.2f;
        audioSource.clip = clip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
