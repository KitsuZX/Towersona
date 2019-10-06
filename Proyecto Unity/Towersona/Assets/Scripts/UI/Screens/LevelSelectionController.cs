using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionController : MonoBehaviour
{
    public void loadLevel(int levelId)
	{
		SceneController.LoadScene(levelId);
	}
}
