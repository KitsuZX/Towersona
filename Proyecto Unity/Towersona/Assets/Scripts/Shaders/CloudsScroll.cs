using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsScroll : MonoBehaviour
{
    // Scroll main texture based on time

    [SerializeField] private float scrollSpeed = 0.01f;
	[SerializeField] private bool rotate = true;

    Renderer rend;

	private Transform swivel;
	private CameraController controller;

	private Quaternion initialRotation;

	bool awaken;

	private void Awake()
	{
		swivel = Camera.main.transform.parent.parent;
		controller = Camera.main.GetComponentInParent<CameraController>();

		awaken = true;

		controller = Camera.main.transform.parent.parent.parent.GetComponent<CameraController>();
		controller.onCameraZoomed.AddListener(delegate () { CameraHasZoomed(); });

		CameraHasZoomed();

		initialRotation = transform.rotation;
	}

	void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);

		if (!rotate && transform.rotation != initialRotation) transform.rotation = initialRotation;
    }

	public void CameraHasZoomed()
	{
		if (!awaken || !rotate) return;		

		float rotation = - (90 - swivel.transform.eulerAngles.x);
		transform.rotation = Quaternion.Euler(rotation, 0f, 0f);		
	}
}
