using UnityEngine;

public class CameraController : MonoBehaviour
{
	Transform swivel, stick;

	public float stickMinZoom, stickMaxZoom;
	public float swivelMinZoom, swivelMaxZoom;

	public float zoomSpeed = 0.1f;
	public float moveSpeedMinZoom, moveSpeedMaxZoom;

	float zoom = 1f;

	private Transform m_camera;

	private Vector3 i;
	private Vector3 lookAt;
	private float d;

	private Vector3 L;

	private Vector3 A;
	private Vector3 B;
	private Vector3 C;
	private Vector3 D;

	private Vector3 minZ;
	private Vector3 maxZ;
	private Vector3 minX;
	private Vector3 maxX;

	private void Awake()
	{
		swivel = transform.GetChild(0);
		stick = swivel.GetChild(0);
		m_camera = stick.GetChild(0);

		RecalculateBounds();	
		ClampPosition(transform.position);
	}

	void Update()
	{
		float zoomDelta = 0;
#if UNITY_EDITOR
		zoomDelta = Input.GetAxis("Mouse ScrollWheel");
		if (zoomDelta != 0f)
		{
			AdjustZoom(zoomDelta);
		}

		float xDelta = Input.GetAxis("Horizontal");
		float zDelta = Input.GetAxis("Vertical");
		if (xDelta != 0f || zDelta != 0f)
		{
			AdjustPosition(xDelta, zDelta);
		}
#endif
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
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

		float distance = Mathf.Lerp(stickMinZoom, stickMaxZoom, zoom);
		stick.localPosition = new Vector3(0f, 0f, distance);

		float angle = Mathf.Lerp(swivelMinZoom, swivelMaxZoom, zoom);
		swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);

		RecalculateBounds();

		ClampPosition(transform.position);
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
		i = m_camera.transform.position;
		i.y = LevelBounds.Instance.position.y;

		d = Vector3.Distance(m_camera.position, i);
		Vector3 vectorDistancia = m_camera.position - i;

		lookAt = m_camera.forward;

		float num = vectorDistancia.x * (m_camera.position.x - i.x) + vectorDistancia.y * (m_camera.position.y - i.y) + vectorDistancia.z * (m_camera.position.z - i.z);
		float den = vectorDistancia.x * lookAt.x + vectorDistancia.y * lookAt.y + vectorDistancia.z * lookAt.z;

		float t = num / den;

		L = new Vector3(m_camera.position.x - lookAt.x * t,
						m_camera.position.y - lookAt.y * t,
						m_camera.position.z - lookAt.z * t);

		float alpha = 90f - Camera.main.fieldOfView * 0.5f - m_camera.transform.rotation.eulerAngles.x;

		float r = d * Mathf.Tan(Mathf.Deg2Rad * alpha);
		B = i + Vector3.forward * r;
		A = i + Vector3.forward * Mathf.Tan(Mathf.Deg2Rad * (alpha + Camera.main.fieldOfView)) * d;

		float HFOV = 2 * Mathf.Atan(Mathf.Tan(Mathf.Deg2Rad * (Camera.main.fieldOfView * 0.5f)) * Camera.main.aspect);

		C = L + Vector3.right * Mathf.Tan(HFOV * 0.5f) * d;
		D = L + Vector3.left * Mathf.Tan(HFOV * 0.5f) * d;
	}

	Vector3 ClampPosition(Vector3 position)
	{
		/*Vector3 top = new Vector3(A.x, A.y, LevelBounds.Instance.topLeft.transform.position.z);
		float d = Vector3.Distance(A, top);

		maxZ = m_camera.position + Vector3.forward * d;

		Vector3 bottom = new Vector3(B.x, B.y, LevelBounds.Instance.bottomRight.position.z);
		d = Vector3.Distance(B, bottom);

		minZ = m_camera.position + Vector3.back * d;

		Vector3 left = new Vector3(LevelBounds.Instance.bottomLeft.position.x, D.y, D.z);
		d = Vector3.Distance(D, left);

		minX = m_camera.position + Vector3.left * d;

		Vector3 right = new Vector3(LevelBounds.Instance.bottomRight.position.x, C.y, C.z);
		d = Vector3.Distance(C, right);

		maxX = m_camera.position + Vector3.right * d;

		position.x = Mathf.Clamp(position.x, minX.x, maxX.x);
		position.z = Mathf.Clamp(position.z, minZ.z, maxZ.z);*/

		return position;
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(i, 1f);
			Gizmos.DrawLine(m_camera.position, i);

			Gizmos.color = Color.red;
			Gizmos.DrawLine(m_camera.position, m_camera.position + lookAt * 50);
			Gizmos.DrawSphere(L, 1f);
			//Gizmos.DrawLine(m_camera.position, m_camera.up);

			Gizmos.color = Color.green;

			Gizmos.DrawSphere(A, 1f);
			Gizmos.DrawSphere(B, 1f);		
			Gizmos.DrawSphere(C, 1f);
			Gizmos.DrawSphere(D, 1f);

			Gizmos.color = Color.black;
			Gizmos.DrawSphere(maxZ, 1f);
			Gizmos.DrawSphere(minZ, 1f);
			Gizmos.DrawSphere(maxX, 1f);
			Gizmos.DrawSphere(minX, 1f);
		}
	}	


}
