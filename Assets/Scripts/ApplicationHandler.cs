using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ApplicationHandler : MonoBehaviour
{
    [SerializeField] [Range(0,0.5f)] private float timeToSceneChange = 0;
    private bool toSceneChange = true;
    private float timeToChange = 0;
    private string sceneToChangeTo;

    public void ChangeScene(string Scene)
    {
        sceneToChangeTo = Scene;
        
    }

    private void Update()
    {
        timeToChange += Time.deltaTime;
        if(timeToChange > timeToSceneChange)
            SceneManager.LoadSceneAsync(sceneToChangeTo, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
