using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
	[Range(0, 5)]
	public float offsetRange = 1;

	Vector3 pos;

	private void Start()
	{
		pos = transform.localPosition;
		pos.x += Random.Range(-offsetRange, offsetRange);
		transform.localPosition = pos;
	}

}
