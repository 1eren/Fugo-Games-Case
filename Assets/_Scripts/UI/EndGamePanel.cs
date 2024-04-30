using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : UIPanelBase
{
    [SerializeField]private Button retryButton;

    private void Start()
    {
        retryButton.onClick.AddListener(SceneController.Instance.RestartGame);
    }
    private void OnEnable()
    {
        GameManager.Instance?.OnGameEnd.AddListener(ShowPanel);
    }
    private void OnDisable()
    {
        GameManager.Instance?.OnGameEnd.RemoveListener(ShowPanel);
    }
}
