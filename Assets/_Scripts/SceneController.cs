using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneController : Singleton<SceneController>
{
    [HideInInspector]
    public UnityEvent OnSceneLoaded = new UnityEvent();

    [HideInInspector]
    public SceneEvent OnSceneInfo = new SceneEvent();

    private void Start()
    {
        OnSceneLoaded.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  
}

public class SceneEvent : UnityEvent<Scene, bool> { }
