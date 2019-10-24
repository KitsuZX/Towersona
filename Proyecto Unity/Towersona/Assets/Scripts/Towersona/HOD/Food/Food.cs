using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draggable), typeof(ReturnToPointAfterCountdown))]
public class Food : MonoBehaviour
{
    //Raycast constants
    private const int RAYCAST_HIT_ARRAY_SIZE = 1;
    private const float MAX_RAYCAST_DISTANCE = 50;

    //Inspector
    public float HungerFulmilmentPerRation => _hungerFulmilmentPerRation;
    [SerializeField] float _hungerFulmilmentPerRation = 1;

    //References
    private new Transform transform;
    private Renderer[] renderers;
    private ReturnToPointAfterCountdown returnToPoint;

    //Cached raycast stuff
    private RaycastHit[] hits;
    LayerMask raycastLayerMask;
    

    public void OnLetGo(Draggable.DraggableEventArgs evt)
    {
        //If we're over a Feedable, feed them and do out Eaten stuff.
        Ray ray = new Ray(evt.cameraPosition, transform.position - evt.cameraPosition);
        int hitCount = Physics.RaycastNonAlloc(ray, hits, MAX_RAYCAST_DISTANCE, raycastLayerMask, QueryTriggerInteraction.Collide);

        Feedable feedable;
        for (int i = 0; i < hitCount; i++)
        {
            feedable = hits[i].collider.GetComponent<Feedable>();
            if (feedable)
            {
                feedable.Feed(this);
                OnEaten();
                break;
            }
        }
    }


    private void OnEaten()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = false;
        }

        returnToPoint.OnReturnedToPoint.AddListener(OnRespawned);
    }

    private void OnRespawned()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = true;
        }

        returnToPoint.OnReturnedToPoint.RemoveListener(OnRespawned);
    }


    private void Awake()
    {
        //Gather references
        transform = GetComponent<Transform>();
        renderers = GetComponentsInChildren<Renderer>();
        returnToPoint = GetComponent<ReturnToPointAfterCountdown>();

        //Ensure correct layer setup
        int feedableLayer = LayerMask.NameToLayer(Feedable.FEEDABLE_LAYER_NAME);
        Debug.Assert(gameObject.layer != feedableLayer, 
            $"Food components must not be in {Feedable.FEEDABLE_LAYER_NAME}.If they are, they will hit themselves with their raycasts.", this);

        //Create necessary stuff
        hits = new RaycastHit[RAYCAST_HIT_ARRAY_SIZE];
        raycastLayerMask = LayerMask.GetMask(Feedable.FEEDABLE_LAYER_NAME);
    }
}
