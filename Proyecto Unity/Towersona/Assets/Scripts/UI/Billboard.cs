using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

	private Transform swivel;
	private CameraController controller;

	public float minScale, maxScale;

	bool awaken;

	private void Awake()
	{
		swivel = Camera.main.transform.parent.parent;
		controller = Camera.main.GetComponentInParent<CameraController>();		

		awaken = true;

		controller = Camera.main.transform.parent.parent.parent.GetComponent<CameraController>();
		controller.onCameraZoomed.AddListener(delegate () { CameraHasZoomed(); });

		CameraHasZoomed();
	}

	public void CameraHasZoomed()
    {
		if (!awaken) return;

		float rotation = 90 - (90 - swivel.transform.eulerAngles.x);
		transform.rotation = Quaternion.Euler(rotation, 0f, 0f);

		float scale = Mathf.Lerp(minScale, maxScale, controller.zoom);
		transform.localScale = new Vector3(scale, scale, scale);
    }
}
