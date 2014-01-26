using UnityEngine;
using System.Collections;

public class hitObject : MonoBehaviour {

	public AudioClip sound;
	public LayerMask layerToHit;

	private float elapsedTime;
	private int readyToCollide;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		readyToCollide = 0;
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = sound;
	}
	
	// Update is called once per frame
	void Update () {
		readyToCollide += 1;
		Debug.Log(readyToCollide);
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if ((1 << hit.gameObject.layer) == layerToHit.value) {
			if (readyToCollide > 60) {
				audioSource.Play();
				readyToCollide = 0;
			}
		}	
	}
}
