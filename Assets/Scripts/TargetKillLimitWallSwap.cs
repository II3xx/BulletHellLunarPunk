using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TargetKillLimitWallSwap : MonoBehaviour
{
    [SerializeField] Collider2D ToggleCollider;
    [SerializeField] [Range(0,10)] int TotalKills = 0;
    [SerializeField] [Range(0.1f, 0.8f)] float shakeAmount = 0.1f;
    [SerializeField] CinemachineVirtualCamera virtCam;
    [SerializeField] AudioClip audioClip;

    private void Awake()
    {
        ToggleCollider = GetComponent<Collider2D>();
        ToggleCollider.isTrigger = false;
    }

    public void OnKillUpdate()
    {
        TotalKills--;
        if(TotalKills <= 0)
        {
            ToggleCollider.isTrigger = true;
            if(virtCam != null)
                StartCoroutine(OnKillCompletion());
            if(audioClip != null)
                gameObject.AddComponent<TemporaryAudioSource>().setClipAndPlay(audioClip);
        }
    }

    IEnumerator OnKillCompletion()
    {
        float shaker = shakeAmount;
        virtCam.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CinemachineBasicMultiChannelPerlin virtCamNoise = virtCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        for (;shaker>0; shaker=-Time.deltaTime)
        {
            virtCamNoise.m_AmplitudeGain = shaker;
            yield return null;
        }
        virtCam.DestroyCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        yield break;
    }
}
