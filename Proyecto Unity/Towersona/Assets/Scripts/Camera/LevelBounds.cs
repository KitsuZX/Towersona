using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
	[SerializeField] private Transform bottomLeftBorder;
	[SerializeField] private Transform topRightBorder;

	[SerializeField] private Transform bottomLeftBound;
	[SerializeField] private Transform topRightBound;

	[HideInInspector] public Vector3 BottomLeftBorder { get { return bottomLeftBorder.position; } }
	[HideInInspector] public Vector3 TopRightBorder { get { return topRightBorder.position; } }
	[HideInInspector] public Vector3 TopLeftBorder { get { return new Vector3(BottomLeftBorder.x, BottomLeftBorder.y, TopRightBorder.z); } }
	[HideInInspector] public Vector3 BottomRightBorder { get { return new Vector3(TopRightBorder.x, BottomLeftBorder.y, BottomLeftBorder.z); } }

	[HideInInspector] public Vector3 BottomLeftBound { get { return bottomLeftBound.position; } }
	[HideInInspector] public Vector3 TopRightBound { get { return topRightBound.position; } }
}
