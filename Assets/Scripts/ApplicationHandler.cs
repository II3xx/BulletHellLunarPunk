using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationHandler : MonoBehaviour
{
    public void ChangeScene(string Scene)
    {
        SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
