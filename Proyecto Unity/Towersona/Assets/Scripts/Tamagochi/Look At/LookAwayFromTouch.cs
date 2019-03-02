using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAwayFromTouch : MonoBehaviour
{
    [SerializeField]
    private Camera cameram;
    [SerializeField]
    private float lookAtDepth = 3;
    [SerializeField] [Range(0, 1)]
    private float interpolation;

    private new Transform transform;

    private Vector3 TouchInWorldSpace
    {
        get
        {
            if (Input.touchCount > 0)
            {
                Transform cameraTrans = cameram.GetComponent<Transform>();
                Vector3 offset = lookAtDepth * (cameraTrans.localToWorldMatrix * new Vector4(0, 0, -1, 0));

                return cameram.ScreenToWorldPoint(new Vector3(
                        Input.GetTouch(0).position.x,
                        Input.GetTouch(0).position.y,
                        transform.position.z - cameraTrans.position.z)) + offset;
            }
            else return Vector3.zero;
        }
    }


    private void LateUpdate()
    {
        Vector3 diff = TouchInWorldSpace - transform.position;
        Quaternion lookAt = Quaternion.LookRotation(diff, Vector3.up);

        Quaternion opposite = Quaternion.Inverse(lookAt);

        transform.rotation = Quaternion.Slerp(lookAt, opposite, interpolation);
    }

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }
}
