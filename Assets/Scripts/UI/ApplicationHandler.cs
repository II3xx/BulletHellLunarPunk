using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ApplicationHandler : MonoBehaviour
{
    [SerializeField] [Range(0,8)] private float timeToSceneChange = 0;
    [SerializeField] private Image fade;
    private bool toSceneChange = false;
    private string sceneToChangeTo;

    IEnumerator SceneFade()
    {
        fade.gameObject.SetActive(true);
        if(timeToSceneChange != 0)
        {
            float oneDivFill = 1 / timeToSceneChange;
            for (float i = 0; i <= timeToSceneChange; i += Time.deltaTime)
            {
                fade.color = new(0, 0, 0, fade.color.a + oneDivFill * Time.deltaTime);
                yield return null;
            }
        }
        SceneManager.LoadSceneAsync(sceneToChangeTo, LoadSceneMode.Single);
    }

    public void ChangeScene(string Scene)
    {
        if (!toSceneChange)
        {
            toSceneChange = true;
            sceneToChangeTo = Scene;
            StartCoroutine(SceneFade());
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
