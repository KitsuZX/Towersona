using UnityEngine;
using UnityEditor;

public class LevelEditorWindow : EditorWindow
{
    int numWaves;
    Texture2D texture;

    Color color = new Color(255f/255f, 165f/255f, 0f/255f);

    [MenuItem("Window/Editor de niveles")]
    public static void ShowWindow()
    {
        LevelEditorWindow window = GetWindow<LevelEditorWindow>("Editor de niveles");
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    private void OnGUI()
    {
        /*GUILayout.Label("Fondo", EditorStyles.boldLabel);

        myString = EditorGUILayout.TextField("Name", myString);*/     

        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();

        //Rect rect = new Rect(i + Screen.width / (i + 1), 0, Screen.width / (i + 1), Screen.height);
        Rect rect = new Rect(0, 0, Screen.width, 50);
        GUI.DrawTexture(rect, texture);

        numWaves = EditorGUILayout.IntField("Numero de oleadas", numWaves);

        if(GUILayout.Button("Crear oleadas"))
        {
            SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
            Debug.Log(spawnPoints.Length);

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                Color rndColor = Random.ColorHSV();

                texture = new Texture2D(1, 1);               
                texture.SetPixel(0, 0, rndColor);
                texture.Apply();

                //Rect rect = new Rect(i + Screen.width / (i + 1), 0, Screen.width / (i + 1), Screen.height);
                Rect textureRect = new Rect(0, 100, Screen.width, 50);
                GUI.DrawTexture(textureRect, texture);         

            }


            GUILayout.Label("Oleadas", EditorStyles.boldLabel);
         
        }      
    }

    
}
