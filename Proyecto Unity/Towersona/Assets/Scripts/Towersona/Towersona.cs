using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackPattern), typeof(TowersonaLODAnimation), typeof(NotificationsManager))]
public class Towersona : MonoBehaviour
{
    [Header("References")]
    public GameObject towersonaModel;
    public Mesh[] lowpolyModels;
    public Mesh[] highpolyModels;
    public AttackPattern pattern;

    [HideInInspector]
    public Tile tile;   
    [HideInInspector]
    public DetailedTowersonaView detailedTowersonaView;
    [HideInInspector]
    public Transform firePoint;
    [HideInInspector]
    public TowersonaNeeds towersonaNeeds;
    [HideInInspector]
    public TowersonaLevel towersonaLevel = TowersonaLevel.LVL1;

    [Header("Parameters")]
    public int cost = 45;

    [Header("Transform parameters")]   
    [SerializeField]
    private float turnSpeed = 1f;
    [SerializeField]
    private Transform[] partsToRotate;    

    private BuildManager towersController;
    private World world;
    private GameManager gameManager;
    private MeshFilter meshFilter;
   

    private void Awake()
    {
        //References
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");

        towersController = gm.GetComponent<BuildManager>();
        gameManager = gm.GetComponent<GameManager>();
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

        meshFilter = GetComponentInChildren<MeshFilter>();
        firePoint = transform.Find("FirePoint");

        //Spawn towersona needs sceene and save a reference
        detailedTowersonaView = towersController.SpawnDetailedTowersonaView(this);
        towersonaNeeds = detailedTowersonaView.GetComponentInChildren<TowersonaNeeds>();

        GetComponent<AudioSource>().Play();
    }

    public void Upgrade()
    {
        //TODO: Upgrading
        print("Upgrading =^.^=");
        SwitchModel(lowpolyModels[1]);
        detailedTowersonaView.Upgrade(highpolyModels[1]);

        towersonaLevel = TowersonaLevel.LVL2;
    }

    public void Evolve(int evolution)
    {
        //TODO: evolving
        print("Evolving to evolution " + (evolution + 1));
        //SwitchModel(lowpolyModels[evolution + 2]);
    }

    /// <summary>
    /// Rotates the model to look to a given target
    /// </summary>
    public void LockOnTarget(Transform target)
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            for (int i = 0; i < partsToRotate.Length; i++)
            {
                Vector3 rotation = Quaternion.Lerp(partsToRotate[i].rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                partsToRotate[i].rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }
    }    

    private void OnMouseUpAsButton()
    {
        gameManager.ChangeCamera(detailedTowersonaView.GetComponentInChildren<Camera>());
        world.SelectTile(tile);

        towersController.SelectTowersona(this);
    }

    private void SwitchModel(Mesh model)
    {
        meshFilter.mesh = model;
    }

    public enum TowersonaLevel
    {
        LVL1, LVL2, LVL31, LVL32
    }
}
