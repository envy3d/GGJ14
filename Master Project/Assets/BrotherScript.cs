using UnityEngine;
using System.Collections;

public class BrotherScript : MonoBehaviour {

	public float colorChangerRadius;
	public LaserScript laser;
	public float timer = 10;
	public LayerMask layerToTarget;

	private float currentTime = 0;

	void changeColor()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, colorChangerRadius, layerToTarget);
		if (colliders.Length > 0)
		{
			int randIdx = Random.Range(0, colliders.Length);
			ObjectLightChanger olc = colliders[randIdx].gameObject.GetComponent("ObjectLightChanger") as ObjectLightChanger;

			laser.fire(colliders[randIdx].transform.position);
			olc.changeColorToRandom ();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		if (currentTime >= timer)
		{
			changeColor ();
			currentTime = 0;
		}
	}
}
