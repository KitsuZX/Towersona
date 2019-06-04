using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySum : MonoBehaviour
{
	public float secsAlive;

	private void Awake()
	{
		Invoke("Destroy", secsAlive);
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}
}
