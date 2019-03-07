using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackPattern), typeof(TowersonaAnimations), typeof(NotificationsManager))]
public class Towersona : MonoBehaviour
{

    [Header("References")]
    public GameObject towersonaModel;
    
    [HideInInspector]
    public Tile tile;   
    [HideInInspector]
    public Camera towersonaNeedsCamera;
    [HideInInspector]
    public Color color;
    [HideInInspector]
    public TowersonaNeeds towersonaNeeds;
    [HideInInspector]
    public Transform firePoint;

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
        towersonaNeeds = towersController.SpawnDetailedTowersonaView(this);
        towersonaNeedsCamera = towersonaNeeds.transform.parent.GetComponentInChildren<Camera>();

        GetComponent<AudioSource>().Play();
    }

    private void LevelUp()
    {
        //TODO: leveling up
    }

    private void Evolve()
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
        gameManager.ChangeCamera(this);
        world.SelectTile(tile);      
    }
}
