using UnityEngine;
using System.Collections;

public class endlevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter () {
		AutoFade.LoadLevel ("mainmenu", 1, 1, Color.white);
	}
}
