using UnityEngine;

#pragma warning disable 649
public class LevelSelectionController : MonoBehaviour
{
	[SerializeField]
	private LevelSelectionButton[] levelSelectionButtons;

	private void Awake()
	{
		LevelData[] levelsData = SaveSystem.levelsData;

		for (int i = 0; i < levelSelectionButtons.Length; i++)
		{
			levelSelectionButtons[i].button.interactable = levelsData[i].avaible;
			levelSelectionButtons[i].score.text = levelsData[i].score.ToString();
		}
	}

	public void loadLevel(int levelId)
	{
		//RelevantUserInfo.currentLevel = levelId - 1;
		SceneController.LoadScene("Level_" + levelId);
	}
}
