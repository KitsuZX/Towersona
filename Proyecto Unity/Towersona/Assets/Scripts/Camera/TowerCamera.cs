using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCamera : MonoBehaviour
{	
	[SerializeField] private LevelBounds levelBounds = null;
	private new Camera camera;

	private void Awake()
	{
		//Regula automáticamente el FOV dependiendo del tamaño del mapa
		/*camera = GetComponent<Camera>();

		Vector3 i = transform.position;
		i.y = levelBounds.transform.position.y;

		float d = Vector3.Distance(transform.position, i);

		float dist = Vector3.Distance(new Vector3(i.x, 0, 0), new Vector3(levelBounds.TopRightBound.x, 0, 0));

		float HFOV = 2 * Mathf.Atan(Mathf.Tan(Mathf.Deg2Rad * (Camera.main.fieldOfView * 0.5f)) * Camera.main.aspect);
		
		float alpha = Mathf.Atan(dist / d) - HFOV * 0.5f;

		float newHFOV =  HFOV + (alpha * 2);

		float newFOV = Mathf.Atan(Mathf.Tan(newHFOV * 0.5f) / Camera.main.aspect) * 2;	

		camera.fieldOfView = newFOV * Mathf.Rad2Deg;*/
		
	}
}
