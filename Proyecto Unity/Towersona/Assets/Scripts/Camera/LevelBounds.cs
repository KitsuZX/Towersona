using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
	public static LevelBounds Instance { get; private set; }

	public Transform bottomRight;
	public Transform topRight;
	public Transform topLeft;
	public Transform bottomLeft;

	public Vector3 position;

	private void Awake()
	{
		if (!Instance) Instance = this;
		else Destroy(this);

		position = transform.position;
	}
}
