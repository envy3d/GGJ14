using UnityEngine;
using System.Collections;

public class FootstepController : MonoBehaviour {

	public float distanceBetweenSteps;
	public AudioSource[] stepAudio;

	private int stepIdx;

	private Vector3 prevPosition;
	private float distanceAccumulator;

	// Use this for initialization
	void Start () {
		//stepAudio = gameObject.GetComponents<AudioSource>();
		stepIdx = 0;
		prevPosition = transform.position;
		prevPosition.y = 0;
		distanceAccumulator = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		newPosition.y = 0;
		distanceAccumulator += (newPosition - prevPosition).magnitude;
		prevPosition = newPosition;

		if (distanceAccumulator > distanceBetweenSteps) {
			distanceAccumulator -= distanceBetweenSteps;
			stepAudio[stepIdx].Play();
			stepIdx++;
			if (stepIdx == stepAudio.Length) {
				stepIdx = 0;
			}
		}
	}
}
