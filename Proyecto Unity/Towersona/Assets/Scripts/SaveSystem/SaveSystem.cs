using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
	private static int numOfLevels = 3;
	public static string levelsFilePath = Path.Combine(Application.persistentDataPath, "levels.tow");

	public static LevelData[] levelsData = new LevelData[numOfLevels];

	public static bool LevelsFileCreated
	{
		get
		{
			return File.Exists(levelsFilePath);
		}
	}

	public static void SaveLevel(int levelIndex, int score)
	{		
		BinaryFormatter formatter = new BinaryFormatter();

		FileStream stream = new FileStream(levelsFilePath, FileMode.Create);

		if (!LevelsFileCreated || levelsData[0] == null)
		{
			CreateLevelsFile();
		}	

		//Check if first completed
		if (levelsData[levelIndex].Completed == false)
		{
			//Set next level avaible
			if(levelIndex < levelsData.Length - 1)
			{
				levelsData[levelIndex + 1].avaible = true;
			}
		}

		//Check if new score
		if (score > levelsData[levelIndex].score){
			levelsData[levelIndex].score = score;
		}		
		
		formatter.Serialize(stream, levelsData);
		stream.Close();
	}

	public static void LoadLevelsData()
	{
		if (File.Exists(levelsFilePath))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(levelsFilePath, FileMode.Open);

			levelsData = formatter.Deserialize(stream) as LevelData[];
			stream.Close();			
		}
		else
		{
			Debug.LogError("Savel file not found in " + levelsFilePath);			
		}
	}

	public static void CreateLevelsFile()
	{		
		for (int i = 0; i < numOfLevels; i++)
		{
			levelsData[i] = new LevelData(i, 0, (i == 0) ? true : false);
		}

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(levelsFilePath, FileMode.Create);

		formatter.Serialize(stream, levelsData);
		stream.Close();
	}
}
