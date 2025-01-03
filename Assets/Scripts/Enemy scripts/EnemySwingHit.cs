using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwingHit : MonoBehaviour
{
    [SerializeField] private int swingDamage;
    [SerializeField] AudioClip audioClipUnitHit;

    private void CreateTempAudio(AudioClip audioClip)
    {
        var temp = GameObject.CreatePrimitive(PrimitiveType.Plane);
        temp.transform.position = transform.position;
        var tempaudio = temp.AddComponent<TemporaryAudioSource>();
        tempaudio.setClipAndPlay(audioClip);
        temp.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScript playerScript = collision.GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            UnitAudioHit();
            playerScript.Damage = swingDamage;
        }
    }

    private void UnitAudioHit()
    {
        if (audioClipUnitHit != null)
        {
            CreateTempAudio(audioClipUnitHit);
        }
    }
}
