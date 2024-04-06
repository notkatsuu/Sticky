using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void Awake()
    {
        PlayerInput.OnResetSceneInput += ReloadScene;
    }

    public void NextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        try
        {
            SceneManager.LoadScene(currentScene + 1);
        }catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
    }

    public void LoadScene(string sceneName)
    {
        try
        {
            SceneManager.LoadScene(sceneName);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void ReloadScene()
    {
        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void Exit()
    {
        FindAnyObjectByType<Score>().ResetScore();
        Debug.Log("Linguini is out!");
        Application.Quit();
    }
}
