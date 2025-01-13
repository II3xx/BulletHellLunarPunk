using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemporaryText : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    public void OnStart(string text)
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = text;
        transform.parent = GameObject.Find("UI").transform;
        transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(TextTimer());
    }

    IEnumerator TextTimer()
    {
        
        for(float i = 0; i < 2f; i += Time.deltaTime)
        {
            transform.position += new Vector3(0, 0.005f, 0);
            textMesh.alpha = textMesh.alpha - 0.005f;
            yield return null;
        }
        Destroy(gameObject);
        yield break;
    }
}
