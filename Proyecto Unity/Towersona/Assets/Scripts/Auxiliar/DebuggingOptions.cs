using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

public enum PriorizationOption { First, Closer, Last, Random }
