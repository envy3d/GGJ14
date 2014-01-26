using UnityEngine;
using System.Collections;

public class menuHACK : MonoBehaviour {

	public GameObject thePyramid;

	// Use this for initialization
	void Start () {
		gameObject.renderer.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Mouse ScrollWheel") != 0)
			renderer.enabled = true;
	}
}
