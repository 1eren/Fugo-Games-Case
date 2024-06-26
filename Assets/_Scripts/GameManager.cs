using AliceGames.Managers;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{

    public GamePrefencesData gamePrefences;

    [HideInInspector]
    public UnityEvent OnGameStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnGameEnd = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStageSuccess = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStageFail = new UnityEvent();

    public bool isFailed;

    private bool isGameStarted;
    [ReadOnly]
    [ShowInInspector]
    public bool IsGameStarted { get { return isGameStarted; } set { isGameStarted = value; } }

    private bool isStageCompleted;
    [ReadOnly]
    [ShowInInspector]
    public bool IsStageCompleted { get { return isStageCompleted; } set { isStageCompleted = value; } }

    private void OnEnable()
    {
        SceneController.Instance?.OnSceneLoaded.AddListener(() => IsStageCompleted = false);
    }

    private void OnDisable()
    {
        SceneController.Instance?.OnSceneLoaded.RemoveListener(() => IsStageCompleted = false);
    }

    public void StartGame()
    {
        if (isGameStarted)
            return;
        isFailed = false;
        isGameStarted = true;
        OnGameStart.Invoke();
    }

    public void EndGame()
    {
        if (!isGameStarted)
            return;
        AdManager.Instance.ShowInterstitial();
        isGameStarted = false;
        OnGameEnd.Invoke();
    }

    /// <summary>
    /// Call it when the player wins or loses the game
    /// </summary>
    /// <param name="value"></param>
    [Button]
    public void CompleteStage(bool value)
    {
        if (IsStageCompleted == true)
            return;

        if (value)
            OnStageSuccess.Invoke();
        else OnStageFail.Invoke();

        IsStageCompleted = true;
    }
}
