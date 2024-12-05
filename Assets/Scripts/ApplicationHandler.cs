using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ApplicationHandler : MonoBehaviour
{
    [SerializeField] [Range(0,8)] private float timeToSceneChange = 0;
    private bool toSceneChange = false;
    private float timeToChange = 0;
    private string sceneToChangeTo;

    public void ChangeScene(string Scene)
    {
        toSceneChange = true;
        sceneToChangeTo = Scene;
    }

    private void Update()
    {
        if (!toSceneChange)
            return;
        timeToChange += Time.deltaTime;
        if(timeToChange > timeToSceneChange)
            SceneManager.LoadSceneAsync(sceneToChangeTo, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
