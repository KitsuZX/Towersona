using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingOptions : MonoBehaviour
{
    public static DebuggingOptions Instance { get; private set; }

    public bool spawnEnemies;
    public bool useMoney;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);
    }
}
