using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

	private Transform swivel;
	private CameraController controller;

	public float minScale, maxScale;

	private void Awake()
	{
		swivel = Camera.main.transform.parent.parent;
		controller = Camera.main.GetComponentInParent<CameraController>();
		CameraHasZoomed();
	}

	public void CameraHasZoomed()
    {
		float rotation = 90 - (90 - swivel.transform.eulerAngles.x);
		transform.rotation = Quaternion.Euler(rotation, 0f, 0f);

		float scale = Mathf.Lerp(minScale, maxScale, controller.zoom);
		transform.localScale = new Vector3(scale, scale, scale);
    }
}
