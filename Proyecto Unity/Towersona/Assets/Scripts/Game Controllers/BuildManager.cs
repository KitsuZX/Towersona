using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{   
    public static BuildManager Instance { get; private set; }
  
    public GameObject detailedTowersonaViewPrefab;

    [HideInInspector]
    public List<Towersona> towersonas;
    [HideInInspector]
    public List<TowersonaNeeds> towersonaNeeds;
    [HideInInspector]
    public bool towerAvaible = true;
    /// <summary>
    /// Max number of Towersonas has been reached
    /// </summary>s
    [HideInInspector]
    public bool maxReached = false;
    [HideInInspector]
    public float lastXUsed = 0f;

    [Header("Parameters")]  
    public float timeBetweenTowersonas = 40f;

    [Header("References")]
    [SerializeField]
    private GameObject[] towersonaPrefabs;
  
    [SerializeField]
    private NodeUI nodeUI;
    [SerializeField]
    private GameObject buildEffect;

    //Private parameters
 
    private Stack<Color> towersonaColors;
    private GameObject towersonaToBuild;
    private Towersona towersonaSelected;

    //Private references
    private GameManager gameManager;
    private World world;

    void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);

        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

        gameManager = GetComponent<GameManager>();
       
        towersonas = new List<Towersona>();

        towersonaColors = new Stack<Color>();      
    }

    private void Update()
    {
        //Hides nodeUI if not clicked on it
        if (nodeUI.UIIsActive)
        {          
            //https://answers.unity.com/questions/615771/how-to-check-if-click-mouse-on-object.html
            if (Input.GetMouseButtonDown(0))
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);

                pointerData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                if (results.Count <= 0)               
                {
                    //Check if another towersona was clicked
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        Towersona t = hit.transform.gameObject.GetComponent<Towersona>();
                        if (t)
                        {
                            //A towersona was hit
                            SelectTowersona(t);
                        }
                        else
                        {
                            DeselectTowersona();
                        }                     
                    }
                }
            }
        }
    }

    public void SpawnTowersona(Tile tile)
    {
        if (towersonaToBuild)
        {
            towersonaToBuild.GetComponent<Towersona>().Spawn(tile.transform, towersonaToBuild.name);
            SpawnEffect(buildEffect, tile.transform.position);

            world.SelectTile(tile);         
            towerAvaible = false;

            towersonaToBuild = null;


        }
    }    

    public void SelectTowersonaToBuild(Towersona _towersona)
    {
        towersonaToBuild = _towersona.gameObject;

        DeselectTowersona();
    }

    public void SelectTowersona(Towersona towersona)
    {
        if(towersonaSelected == towersona)
        {            
            return;
        }

        towersonaSelected = towersona;
        towersonaToBuild = null;
         
        nodeUI.SetTarget(towersona);
    }

    public void DeselectTowersona()
    {
        towersonaSelected = null;
        nodeUI.Hide();
    }   

    public void UpgradeTowersona()
    {
        towersonaSelected.Upgrade();
        SpawnEffect(buildEffect, towersonaSelected.transform.position);

        DeselectTowersona();     
    }

    public void Evolve(int evolution)
    {
        towersonaSelected.Evolve(evolution);
        SpawnEffect(buildEffect, towersonaSelected.transform.position);

        DeselectTowersona();
    }

    private void SpawnEffect(GameObject _effect, Vector3 position)
    {
        GameObject effect = Instantiate(_effect, position, Quaternion.identity);
        Destroy(effect, 5f);
    }

    private void OnGUI()
    {
       
        if (towersonaSelected != null)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 32;
            style.fontStyle = FontStyle.Bold;

            AttackPattern pattern = towersonaSelected.towersonaLOD.pattern;

            string message = "";
            message += "Fuerza: " + pattern.currentAttackStrength + "\n";
            message += "V. Ataque: " + pattern.currentAttackSpeed + "\n";
            message += "Rango: " + pattern.currentAttackRange + "\n";
            message += "V. Bala: " + pattern.currentBulletSpeed + "\n";

            GUI.Label(new Rect(Screen.width * 0.66f + 110, 200, 120, 100), message, style);

            Destroy(pattern);
        }

    }
}
