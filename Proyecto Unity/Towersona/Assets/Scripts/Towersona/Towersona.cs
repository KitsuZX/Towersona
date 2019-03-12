using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackPattern), typeof(TowersonaLODAnimation), typeof(NotificationsManager))]
public class Towersona : MonoBehaviour
{
    [Header("References")]
    public GameObject towersonaModel;
    
    [HideInInspector]
    public Tile tile;   
    [HideInInspector]
    public DetailedTowersonaView detailedTowersonaView;
    [HideInInspector]
    public Transform firePoint;
    [HideInInspector]
    public TowersonaNeeds towersonaNeeds;    

    [Header("Parameters")]
    public int cost = 45;

    [Header("Transform parameters")]   
    [SerializeField]
    private float turnSpeed = 1f;
    [SerializeField]
    private Transform[] partsToRotate;    

    private TowersController towersController;
    private World world;
    private GameManager gameManager;

    private void Awake()
    {
        //References
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");

        towersController = gm.GetComponent<TowersController>();
        gameManager = gm.GetComponent<GameManager>();
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

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
    }

    public void Evolve()
    {
        //TODO: evolving
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

    enum TowersonaLevel
    {
        lvl1, lvl2, ev1, ev2
    }
}
