using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class LevelEditorWindow : EditorWindow
{
    int numWaves;
    static Texture2D texture;

    bool backgroundFoldout = true;
    bool pathsFoldout = true;
    bool buildingPlacesFoldOut = true;   
    bool wavesFoldout = true;

    bool showWaves = false;


    Wave[] waves;
    int[,] numGroups;

    Color color = new Color(255f / 255f, 165f / 255f, 0f / 255f);
    static List<Object> paths;
    static List<Object> buildingPlaces;

    [MenuItem("Window/Editor de niveles")]
    public static void ShowWindow()
    {
        LevelEditorWindow window = GetWindow<LevelEditorWindow>("Editor de niveles");
        window.minSize = new Vector2(200, 100);

        if (LevelEditor.Instance.background == null)
        {
            LevelEditor.Instance.ChangeBackground(texture);
        }     

        if(paths == null)
        {
            paths = new List<Object>();
        }


        if (buildingPlaces == null)
        {
            buildingPlaces = new List<Object>();
        }
        window.Show();
    }    
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 15;
        style.alignment = (TextAnchor)TextAlignment.Center;
        style.fontStyle = FontStyle.Bold;

        EditorGUILayout.LabelField("Editor de niveles", style);

        #region DeleteLevel
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Borrar nivel"))
        {
            texture = null;
            paths.Clear();
            buildingPlaces.Clear();
            LevelEditor.Instance.ResetLevel();
        }
        GUI.backgroundColor = Color.white;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        #endregion

        #region Background
        backgroundFoldout = EditorGUILayout.Foldout(backgroundFoldout, "Fondo", EditorStyles.foldoutPreDrop);
        if (backgroundFoldout)
        {
            EditorGUI.BeginChangeCheck();
            texture = TextureField("Fondo", texture);

            if (EditorGUI.EndChangeCheck())
            {
                LevelEditor.Instance.ChangeBackground(texture);
            }
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        #endregion

        #region Paths
        pathsFoldout = EditorGUILayout.Foldout(pathsFoldout, "Caminos", EditorStyles.foldoutPreDrop);
        if (pathsFoldout)
        {
            if (GUILayout.Button("Crear Camino"))
            {
                Object path = LevelEditor.Instance.CreatePath();
                paths.Add(path);
            }

            for (int i = 0; i < paths.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                paths[i] = EditorGUILayout.ObjectField(paths[i], typeof(Object), true);
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Borrar", EditorStyles.radioButton))
                {
                    DestroyObj(paths[i], paths);
                }
                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        #endregion

        #region Building Places    
        buildingPlacesFoldOut = EditorGUILayout.Foldout(buildingPlacesFoldOut, "Sitios de construcción", EditorStyles.foldoutPreDrop);
        if (buildingPlacesFoldOut)
        {
            if (GUILayout.Button("Crear sitio de construcción"))
            {
                Object buildingPlace = LevelEditor.Instance.CreateBuildingSite();
                buildingPlaces.Add(buildingPlace);
            }

            for (int i = 0; i < buildingPlaces.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                buildingPlaces[i] = EditorGUILayout.ObjectField(buildingPlaces[i], typeof(Object), true);
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Borrar", EditorStyles.radioButton))
                {
                    DestroyObj(buildingPlaces[i], buildingPlaces);
                }
                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        #endregion

        #region Waves
        wavesFoldout = EditorGUILayout.Foldout(wavesFoldout, "Oleadas", EditorStyles.foldoutPreDrop);

        if (wavesFoldout)
        {
            numWaves = EditorGUILayout.IntField("Numero de oleadas", numWaves);


            if (GUILayout.Button("Crear Oleadas"))
            {
                showWaves = true;        
                numGroups = new int[numWaves, paths.Count];
            }

            if (showWaves)
            {
                ShowWaves(numWaves, paths.Count);
            }

            if (GUILayout.Button("Borrar oleadas"))
            {
                showWaves = false;                
            }
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        #endregion
    }

    void ShowWaves(int numWaves, int numSpawnPoints)
    {
        for (int row = -1; row < numWaves; row++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int col = -1; col < numSpawnPoints; col++)
            {
                EditorGUILayout.BeginVertical();

                if (row == 0)
                {
                    if (col == 0)
                    {
                        GUILayout.Label("", EditorStyles.centeredGreyMiniLabel);
                    }
                    else
                    {
                        GUILayout.Label("Spawn " + (col), EditorStyles.centeredGreyMiniLabel);
                    }
                }
                else
                {
                    if (col == 0)
                    {
                        GUILayout.Label("Oleada " + row, EditorStyles.label);
                    }
                    else
                    {
                        //numGroups[row, col] = EditorGUILayout.IntField("Num grupos", numGroups[row, col]);

                        /*for (int i = 0; i < numGroups[row, col]; i++)
                        {
                            EditorGUILayout.IntField(0);
                        }*/
                    }
                }


                EditorGUILayout.EndVertical();
            }


            EditorGUILayout.EndHorizontal();
        }

    }

    void DestroyObj(Object obj, List<Object> list)
    {
        list.Remove(obj);
        DestroyImmediate(obj);
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
