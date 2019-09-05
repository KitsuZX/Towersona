using UnityEngine;

public class CameraController : MonoBehaviour
{
	Transform swivel, stick;

	[SerializeField] private LevelBounds levelBounds = null;

	public float stickMinZoom, stickMaxZoom;
	public float swivelMinZoom, swivelMaxZoom;

	public float zoomSpeed = 0.1f;
	public float moveSpeedMinZoom, moveSpeedMaxZoom;
	public float minFOV, maxFOV;

	float zoom = 1f;

	private Transform m_camera;		

	//Points where the porjection intersects de level
	private Vector3 topIntersection;
	private Vector3 bottomIntersection;
	private Vector3 rightIntersection;
	private Vector3 leftIntersection;

	private Vector3 originalPosition;

	private void Awake()
	{
		originalPosition = transform.position;
		swivel = transform.GetChild(0);
		stick = swivel.GetChild(0);

		m_camera = stick.GetChild(0);
			
		float x = (levelBounds.BottomLeftBound.x + levelBounds.TopRightBound.x) * 0.5f;
		float y = transform.position.y; ;
		float z = (levelBounds.BottomLeftBound.z + levelBounds.TopRightBound.z) * 0.5f;

		transform.position = new Vector3(x, y, z);
	}

	private void Start()
	{
		minFOV = Camera.main.fieldOfView;
	}

	void Update()
	{
		float zoomDelta = 0;

#if UNITY_EDITOR
		zoomDelta = Input.GetAxis("Mouse ScrollWheel");
		if (zoomDelta != 0f)
		{
			AdjustZoom(-zoomDelta);
		}

		float xDelta = Input.GetAxis("Horizontal");
		float zDelta = Input.GetAxis("Vertical");
		if (xDelta != 0f || zDelta != 0f)
		{
			AdjustPosition(xDelta, zDelta);
		}
#endif		

		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			AdjustPosition(-touchDeltaPosition.x, -touchDeltaPosition.y);
		}

		if(Input.touchCount == 2)
		{
			Touch firstTouch = Input.GetTouch(0);
			Touch secondTouch = Input.GetTouch(1);

			Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
			Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

			float prevMagnitude = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
			float currentMagnitude = (firstTouch.position - secondTouch.position).magnitude;

			zoomDelta = currentMagnitude - prevMagnitude;

			AdjustZoom(-zoomDelta * zoomSpeed);
		}

	}

	void AdjustZoom(float delta)
	{		
		zoom = Mathf.Clamp01(zoom + delta);
		
		//Si hacemos zoom out
		if (delta > 0 && zoom < 1)
		{
			//Ver en que cuadrante del nivel está centrada la cámara
			Vector3 closestBorderPoint = GetClosestMeshVertex();
			closestBorderPoint.y = transform.position.y;

			float totalDistance = Vector3.Distance(originalPosition, closestBorderPoint);
			float actualDistance = Vector3.Distance(transform.position, originalPosition);	
			
			//Transicionat al centro de la camara
			transform.position = Vector3.Lerp(closestBorderPoint, originalPosition,	zoom);
		}

		float angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, zoom);
		swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);

		float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
		stick.localPosition = new Vector3(0f, angle * 0.75f, distance);

		float FOV = Mathf.Lerp(maxFOV, minFOV, zoom);
		Camera.main.fieldOfView = FOV;

		RecalculateBounds();
	}

	Vector3 GetClosestMeshVertex()
	{
		Vector3 closestPoint = Vector3.zero;
		Mesh plane = levelBounds.GetComponent<MeshFilter>().mesh;

		Vector3[] vertices = plane.vertices;

		float minDistance = Mathf.Infinity;
		for (int i = 0; i < vertices.Length  - 1; i++)
		{
			Vector3 worldPt = transform.TransformPoint(vertices[i]);
			float distance = Vector3.Distance(transform.position, worldPt);
			if(distance < minDistance)
			{
				minDistance = distance;
				closestPoint = worldPt;
			}
		}

		return closestPoint;
	}

	void AdjustPosition(float xDelta, float zDelta)
	{
		Vector3 direction = new Vector3(xDelta, 0f, zDelta).normalized;
		float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
		float distance = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, zoom) *	damping * Time.deltaTime;

		Vector3 position = transform.localPosition;
		position += direction * distance;

		RecalculateBounds();
		
		transform.localPosition = ClampPosition(position);
	}

	void RecalculateBounds()
	{
		Vector3 i = m_camera.transform.position;
		i.y = levelBounds.transform.position.y;

		float d = Vector3.Distance(m_camera.position, i);
		Vector3 vectorDistancia = m_camera.position - i;

		Vector3 lookAt = m_camera.forward;

		float num = vectorDistancia.x * (m_camera.position.x - i.x) + vectorDistancia.y * (m_camera.position.y - i.y) + vectorDistancia.z * (m_camera.position.z - i.z);
		float den = vectorDistancia.x * lookAt.x + vectorDistancia.y * lookAt.y + vectorDistancia.z * lookAt.z;

		float t = num / den;

		Vector3 L = new Vector3(m_camera.position.x - lookAt.x * t,
						m_camera.position.y - lookAt.y * t,
						m_camera.position.z - lookAt.z * t);

		float alpha = 90f - Camera.main.fieldOfView * 0.5f - m_camera.transform.rotation.eulerAngles.x;

		topIntersection = i + Vector3.forward * Mathf.Tan(Mathf.Deg2Rad * (alpha + Camera.main.fieldOfView)) * d;
		bottomIntersection = i + Vector3.forward * Mathf.Tan(Mathf.Deg2Rad * alpha) * d;		

		float HFOV = 2 * Mathf.Atan(Mathf.Tan(Mathf.Deg2Rad * (Camera.main.fieldOfView * 0.5f)) * Camera.main.aspect);

		rightIntersection = L + Vector3.right * Mathf.Tan(HFOV * 0.5f) * d;
		leftIntersection = L + Vector3.left * Mathf.Tan(HFOV * 0.5f) * d;
	}

	Vector3 ClampPosition(Vector3 position)
	{
		Vector3 maxZ;
		Vector3 minZ;
		Vector3 maxX;
		Vector3 minX;
		
		Vector3 top = new Vector3(topIntersection.x, topIntersection.y,levelBounds.TopRightBorder.z);
		float d = Vector3.Distance(topIntersection, top);

		if (topIntersection.z <= top.z)
		{
			maxZ = transform.position + Vector3.forward * d;
		}
		else
		{
			maxZ = transform.position;
		}

		Vector3 bottom = new Vector3(bottomIntersection.x, bottomIntersection.y, levelBounds.BottomLeftBorder.z);
		d = Vector3.Distance(bottomIntersection, bottom);

		if(topIntersection.z >= bottom.z)
		{
			minZ = transform.position + Vector3.back * d;
		}
		else
		{
			minZ = transform.position;
		}		

		Vector3 left = new Vector3(levelBounds.BottomLeftBorder.x, leftIntersection.y, leftIntersection.z);
		d = Vector3.Distance(leftIntersection, left);

		minX = transform.position + Vector3.left * d;

		Vector3 right = new Vector3(levelBounds.TopRightBorder.x, rightIntersection.y, rightIntersection.z);
		d = Vector3.Distance(rightIntersection, right);

		maxX = transform.position + Vector3.right * d;

		position.x = Mathf.Clamp(position.x, minX.x, maxX.x);
		position.z = Mathf.Clamp(position.z, minZ.z, maxZ.z);

		return position;
	}

	/*private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{	
			Gizmos.color = Color.green;

			Gizmos.color = Color.magenta;
			Gizmos.DrawSphere(topIntersection, 1f);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(bottomIntersection, 1f);
			Gizmos.color = Color.magenta;
			Gizmos.DrawSphere(rightIntersection, 1f);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(leftIntersection, 1f);
		}
		
	}*/
}
