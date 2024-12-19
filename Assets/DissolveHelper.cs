using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveHelper : MonoBehaviour
{
    [SerializeField] Material DissolveMaterial;
    [SerializeField] float dissolveAmount;

    // Update is called once per frame
    void LateUpdate()
    {
        dissolveAmount = Mathf.Clamp01(dissolveAmount);
        DissolveMaterial.SetFloat("_Dissolve", dissolveAmount);
    }
}
