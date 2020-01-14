using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(LookAtConstraint))]
public class LookAtConstraintRestRotationUpdater : MonoBehaviour
{
    private LookAtConstraint constraint;
    private new Transform transform;

    private void LateUpdate()
    {
        constraint.rotationAtRest = transform.localRotation.eulerAngles;
    }

    private void Awake()
    {
        transform = GetComponent<Transform>();
        constraint = GetComponent<LookAtConstraint>();
    }
}
