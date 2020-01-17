using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{	
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

	private void Awake()
	{
		RelevantUserInfo.savingAllowed = true;

		if (!SaveSystem.LevelsFileCreated)
		{
			SaveSystem.CreateLevelsFile();
		}
		else
		{
			SaveSystem.LoadLevelsData();
		}
	}

	public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
