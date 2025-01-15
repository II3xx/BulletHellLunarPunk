using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class TargetKillLimitWallSwap : MonoBehaviour
{
    [SerializeField] Collider2D ToggleCollider;
    [SerializeField] [Range(0,10)] int TotalKills = 0;
    [SerializeField] [Range(0.1f, 0.8f)] float shakeAmount = 0.1f;
    [SerializeField] CinemachineVirtualCamera virtCam;
    [SerializeField] AudioClip audioClip;
    [SerializeField] UnityEvent onActivated;

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
            onActivated.Invoke();
            ToggleCollider.isTrigger = true;
            if(virtCam != null)
                StartCoroutine(OnKillCompletion());
            if (audioClip != null)
                CreateTempAudio(audioClip);
        }
    }

    private void CreateTempAudio(AudioClip audioClip)
    {
        var temp = GameObject.CreatePrimitive(PrimitiveType.Plane);
        temp.transform.position = transform.position;
        var tempaudio = temp.AddComponent<TemporaryAudioSource>();
        tempaudio.SetClipAndPlay2D(audioClip);
        temp.GetComponent<MeshRenderer>().enabled = false;
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
