using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour
{
    [SerializeField] Image HP1, HP2, HP3, dashImage;

    

    public void SetHPUI(int HP)
    {
        switch(HP)
        {
            case 0:
                HP1.fillAmount = 0;
                HP2.fillAmount = 0;
                HP3.fillAmount = 0;
                break;
            case 1:
                HP1.fillAmount = 0.5f;
                HP2.fillAmount = 0;
                HP3.fillAmount = 0;
                break;
            case 2:
                HP1.fillAmount = 1;
                HP2.fillAmount = 0;
                HP3.fillAmount = 0;
                break;
            case 3:
                HP1.fillAmount = 1;
                HP2.fillAmount = 0.5f;
                HP3.fillAmount = 0;
                break;
            case 4:
                HP1.fillAmount = 1;
                HP2.fillAmount = 1;
                HP3.fillAmount = 0;
                break;
            case 5:
                HP1.fillAmount = 1;
                HP2.fillAmount = 1;
                HP3.fillAmount = 0.5f;
                break;
            case 6:
                HP1.fillAmount = 1;
                HP2.fillAmount = 1;
                HP3.fillAmount = 1;
                break;
        }
    }

    public void OnDash(float timeToFill)
    {
        dashImage.fillAmount = 0;
        StartCoroutine(DashFill(timeToFill));
    }

    IEnumerator DashFill(float timeToFill)
    {
        float oneDivFill = 1 / timeToFill;
        for (float i = 0; i <= 1 ;i += (Time.deltaTime * oneDivFill))
        {
            dashImage.fillAmount = i;
            yield return null;
        }
        yield break;
    }
}
