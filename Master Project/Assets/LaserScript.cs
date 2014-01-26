using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	public float fireDuration = 0.125f;
	private float fireTime = 0;
	private Vector3 laserDestination;
	private LineRenderer line;
	private GameObject audioSourceAtTarget;
	private AudioSource[] audioSources1;
	private AudioSource[] audioSources2;

	// Use this for initialization
	void Start () {
	
		line = gameObject.GetComponent<LineRenderer>();
		line.enabled = false;
		audioSourceAtTarget = transform.FindChild("AudioSourceAtTarget").gameObject;
		//AudioSource[] sources = gameObject.GetComponents<AudioSource>();
		audioSources1 = gameObject.GetComponents<AudioSource>();
		audioSources2 = audioSourceAtTarget.GetComponents<AudioSource>();
	}
	
	public void fire(Vector3 destination)
	{
		fireTime = 0;
		line.enabled = true;
		line.SetPosition (0, transform.position);
		audioSources1[Random.Range(0, audioSources1.Length)].Play();
		laserDestination = destination;
		laserDestination.y -= 1;
		line.SetPosition (1, laserDestination);
		audioSourceAtTarget.transform.position = laserDestination;
		audioSources2[Random.Range(0, audioSources1.Length)].Play();

	}

	void Update()
	{
		if (line.enabled)
		{
			fireTime += Time.deltaTime;
			line.SetPosition(0, transform.position);
			laserDestination.y += Time.deltaTime * (2 / fireDuration);
			line.SetPosition(1, laserDestination);
			audioSourceAtTarget.transform.position = laserDestination;

			if (fireTime >= fireDuration)
			{
				fireTime = 0;
				line.enabled = false;
				audioSourceAtTarget.transform.position = transform.position;
			}
		}
	}
}
