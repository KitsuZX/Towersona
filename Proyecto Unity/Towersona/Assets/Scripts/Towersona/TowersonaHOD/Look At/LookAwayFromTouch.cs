using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAwayFromTouch : MonoBehaviour
{
	[HideInInspector] public bool isBeingCaressed = false;
	[HideInInspector] public Camera m_Camera;

    [SerializeField] private float lookAtDepth = 3f;
    [SerializeField] [Range(0, 1)] private float interpolation = 0f;

    private new Transform transform;
    private Vector3 TouchInWorldSpace
    {
        get
        {
            if (Input.touchCount > 0)
            {
                Transform cameraTrans = m_Camera.GetComponent<Transform>();
                Vector3 offset = lookAtDepth * (cameraTrans.localToWorldMatrix * new Vector4(0, 0, -1, 0));

                return m_Camera.ScreenToWorldPoint(new Vector3(
                        Input.GetTouch(0).position.x,
                        Input.GetTouch(0).position.y,
                        transform.position.z - cameraTrans.position.z)) + offset;
            }

            //Mouse controls
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                Transform cameraTrans = m_Camera.GetComponent<Transform>();
                Vector3 offset = lookAtDepth * (cameraTrans.localToWorldMatrix * new Vector4(0, 0, -1, 0));

                return m_Camera.ScreenToWorldPoint(new Vector3(
                       Input.mousePosition.x,
                       Input.mousePosition.y,
                       transform.position.z - cameraTrans.position.z)) + offset;
            }
#endif
            else return Vector3.zero;
        }
    }

    private void Awake()
    {  
        transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
		if (isBeingCaressed)
		{
			Vector3 diff = TouchInWorldSpace - transform.position;
			Quaternion lookAt = Quaternion.LookRotation(diff, Vector3.up);

			Quaternion opposite = Quaternion.Inverse(lookAt);

			transform.rotation = Quaternion.Slerp(lookAt, opposite, interpolation);
		}
    }

  
}
