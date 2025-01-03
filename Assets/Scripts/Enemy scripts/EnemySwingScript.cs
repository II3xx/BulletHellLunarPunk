using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwingScript : MonoBehaviour
{
    [SerializeField] GameObject swingObject;
    [SerializeField] MovingMeleeStats stats;
    private GameObject player;
    private float swingCDRunTime = 0;
    private bool swingReset = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator Swinging()
    {
        swingReset = false;
        swingObject.SetActive(true);
        float arcToSkip = stats.SwingArc / stats.SwingSpeed;
        float maxDistance = Vector2.Distance(swingObject.transform.position, transform.position);
        float startAngleToPlayer = LunarMath.VectorAngle(player.transform.position, transform.position);
        swingObject.transform.localRotation = Quaternion.Euler(0, 0, startAngleToPlayer * Mathf.Rad2Deg + stats.SwingStart);
        for (float runtime = 0; runtime < stats.SwingSpeed; runtime += Time.deltaTime)
        {
            swingObject.transform.localRotation = Quaternion.Euler(0, 0, swingObject.transform.localRotation.eulerAngles.z + arcToSkip * Time.deltaTime);
            float angle = (swingObject.transform.localRotation.eulerAngles.z-90) * Mathf.Deg2Rad;
            swingObject.transform.localPosition = new Vector2(maxDistance * Mathf.Cos(angle), maxDistance * Mathf.Sin(angle));
            yield return null;
        }
        swingObject.SetActive(false);
        swingReset = true;
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if(swingReset)
        {
            swingCDRunTime += Time.deltaTime;
        }
        
        if(swingCDRunTime > stats.SwingCooldown)
        {
            swingCDRunTime = 0;
            StartCoroutine(Swinging());
        }
    }
}
