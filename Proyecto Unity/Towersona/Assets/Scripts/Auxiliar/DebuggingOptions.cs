using UnityEngine;
using System.IO;
using NaughtyAttributes;

public class DebuggingOptions : MonoBehaviour
{
    public static DebuggingOptions Instance { get; private set; }

    public bool spawnEnemies;
    public bool useMoney;
    public PriorizationOption priorizationOption;
	public bool showStats;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);
    }
		
	[Button]
	public void RemoveSavedData()
	{
		string path = SaveSystem.levelsFilePath;
		File.Delete(path);

		Debug.Log("File deleted in " + path);
		
	}
}

public enum PriorizationOption { First, Closer, Last, Random }
