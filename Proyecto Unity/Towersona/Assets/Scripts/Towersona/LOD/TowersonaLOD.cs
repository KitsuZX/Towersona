using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersonaLOD : MonoBehaviour
{
    [HideInInspector] public Towersona towersona;
    public Transform firePoint;
    [HideInInspector] public AttackPattern pattern;

    [Header("Transform parameters")]
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private Transform[] partsToRotate = null;

    private MeshFilter meshFilter;

    private void Awake()
    {
        pattern = GetComponent<AttackPattern>();
        meshFilter = GetComponentInChildren<MeshFilter>();        
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

    public void SwitchModel(Mesh model)
    {
        meshFilter.mesh = model;
    }

    private void OnMouseUpAsButton()
    {
        towersona.TowersonaLODTouched();
    }

}
