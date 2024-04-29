using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : UIPanelBase
{
	public void OnEnable()
	{
        if (SceneController.Instance == null)
            return;
        SceneController.Instance?.OnSceneLoaded.AddListener(ShowTutorial);
        GameManager.Instance?.OnGameStart.AddListener(HidePanel);

    }
    public void OnDisable()
	{
		if (SceneController.Instance == null)
			return;
		SceneController.Instance?.OnSceneLoaded.RemoveListener(ShowTutorial);
		GameManager.Instance?.OnGameStart.RemoveListener(HidePanel);
	}
	private void ShowTutorial()
	{
		base.ShowPanel();
	
	}
	private void HidePanel()
	{
		base.HidePanel();
	}
}
