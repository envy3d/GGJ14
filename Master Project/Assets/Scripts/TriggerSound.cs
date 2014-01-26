using UnityEngine;
using System.Collections;

public class TriggerSound : MonoBehaviour {

	public AudioSource soundToTrigger;
	public bool playMoreThanOnce;

	private bool hasPlayed;

	// Use this for initialization
	void Start () {
		hasPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (!playMoreThanOnce && hasPlayed)
			return;

		if (other.gameObject.tag == "Player")
		{
			soundToTrigger.Play();
			hasPlayed = true;
		}
	}
}
