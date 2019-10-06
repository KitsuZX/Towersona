using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{	
	public static int currentScene = 0;
	public static int sceneToLoad;

	public static void LoadScene(int sceneId)
	{
		sceneToLoad = sceneId;
		SceneManager.LoadScene("Loading Screen");
	}		
}
