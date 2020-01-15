using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    private static LevelData[] levels = new LevelData[3];

    static string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "savedData.tow");

        if (!File.Exists(savePath))
        {
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i] = new LevelData(i, 0, (i == 0)? true : false);
            }            
        }
    }

    public static void SaveLevel(LevelData data)
    {
        levels[data.Index] = data;

        using (var writer = new BinaryWriter(File.Open(savePath, FileMode.Create)))
        {
            writer.Write(levels.Length);
            for (int i = 0; i < levels.Length; i++)
            {             
                writer.Write(levels[i].Index);
                writer.Write(levels[i].Score);
                writer.Write(levels[i].Available);       
            }
        }
    }

    public static void Load()
    {
        using (var reader = new BinaryReader(File.Open(savePath, FileMode.Open)))
        {
            int count = reader.ReadInt32();
            for (int i = 0; i < levels.Length; i++)
            {
                int index = reader.ReadInt32();
                int score = reader.ReadInt32();
                bool available = reader.ReadBoolean();
            }
        }
    }
}