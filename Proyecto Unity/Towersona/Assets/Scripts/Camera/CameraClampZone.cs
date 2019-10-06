using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClampZone : MonoBehaviour
{	
	[SerializeField] private Vector3 minPosition = Vector3.zero;
	[SerializeField] private Vector3 maxPosition = Vector3.zero;
	[SerializeField] private Vector3 minScale = Vector3.zero;
	[SerializeField] private Vector3 maxScale = Vector3.zero;


	private Transform swivel;
	private CameraController controller;

	private void Awake()
	{
		GetComponent<MeshRenderer>().enabled = false;

		controller = Camera.main.transform.parent.parent.parent.GetComponent<CameraController>();
		controller.onCameraZoomed.AddListener(delegate () { OnCameraZoom(); });

		OnCameraZoom();
	}

	public void OnCameraZoom()
	{
		Vector3 position = Vector3.Lerp(minPosition, maxPosition, Mathf.Abs(1 - controller.zoom));
		transform.localPosition = position;	

		Vector3 scale = Vector3.Lerp(minScale, maxScale, Mathf.Abs(1 - controller.zoom));
		transform.localScale = scale;
	}
}
