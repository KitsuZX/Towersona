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
        window.minSize = new Vector2(200, 100);
        window.Show();
    }
    
    private void OnGUI()
    {
        /*GUILayout.Label("Fondo", EditorStyles.boldLabel);

        myString = EditorGUILayout.TextField("Name", myString);  

        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();

        //Rect rect = new Rect(i + Screen.width / (i + 1), 0, Screen.width / (i + 1), Screen.height);
        Rect rect = new Rect(0, 0, Screen.width, 50);
        GUI.DrawTexture(rect, texture);

        numWaves = EditorGUILayout.IntField("Numero de oleadas", numWaves);

        if(GUILayout.Button("Nuevo Spawn Point"))
        {   
            LevelEditor.Instance.NewSpawnPoint();
        }*/

        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Borrar nivel"))
        {
            texture = null;
            LevelEditor.Instance.ResetLevel();
        }
        GUI.backgroundColor = Color.white;

        EditorGUI.BeginChangeCheck();
        texture = TextureField("Fondo", texture);

        if (EditorGUI.EndChangeCheck())
        {
            LevelEditor.Instance.ChangeBackground(texture);
        }


        if (GUILayout.Button("Crear sitio de construcción"))
        {
            LevelEditor.Instance.CreateBuildingSite();
        }

        if (GUILayout.Button("Crear Camino"))
        {
            LevelEditor.Instance.CreatePath();
        }

        GUILayout.Label("OLEADAS (recuerda haber creado los caminos primero)", EditorStyles.boldLabel);

        numWaves = EditorGUILayout.IntField("Numero de oleadas", numWaves);

      
        if (GUILayout.Button("Crear Oleadas"))
        {
            Debug.Log("Abrir ventana de oleadas par " + numWaves + " oleadas");
        }

    }

    private static Texture2D TextureField(string name, Texture2D texture)
    {
        GUILayout.BeginVertical();
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperCenter;
        style.fixedWidth = 70;
        style.fontStyle = FontStyle.Bold;
        GUILayout.Label(name, style);
        var result = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.EndVertical();
        return result;
    }


}
