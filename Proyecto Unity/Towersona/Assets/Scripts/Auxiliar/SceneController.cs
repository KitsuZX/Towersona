using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{	
	public static int currentScene = 0;
	public static string sceneToLoad;
	
	public static void LoadScene(string sceneName)
	{
		sceneToLoad = sceneName;		
		SceneManager.LoadScene("Loading Screen");
	}
}
