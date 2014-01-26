using UnityEngine;
using System.Collections;

public class LightChangerSystem : MonoBehaviour {

	public int steps;
	public string[] colorTags;
	public AudioSource colorChangeSound;

	private int currentColorIndex;
	private int prevColorIndex;
	private int stepCounter;

	// Use this for initialization
	void Start() {
		//steps += 1;
		stepCounter = 0;
		currentColorIndex = 0;
		prevColorIndex = 0;
		ChangeColor();
	}
	
	// Update is called once per frame
	void Update() {
		float wheel = Input.GetAxis("Mouse ScrollWheel");
		if (wheel > 0) {
			stepCounter += 1;
			if (stepCounter == 1) {
				currentColorIndex = (currentColorIndex + 1) % colorTags.Length;
			}
			if (stepCounter >= steps) {
				stepCounter = 0;
				prevColorIndex = currentColorIndex;
			}
			ChangeColor();
		}
		else if (wheel < 0) {
			stepCounter -= 1;
			if (stepCounter == -1) {
				stepCounter = steps - 1;
				if (prevColorIndex == 0)
					prevColorIndex = colorTags.Length - 1;
				else
					prevColorIndex = prevColorIndex - 1;
			}
			if (stepCounter == 0) {
				currentColorIndex = prevColorIndex;
			}
			ChangeColor();
		}
	}

	private void ChangeColor() {
		colorChangeSound.Play ();
		LightChange lightChange;
		lightChange.color1 = colorTags[prevColorIndex];
		lightChange.color2 = colorTags[currentColorIndex];
		lightChange.increment = ((float)stepCounter)/((float)steps);
		BroadcastMessage("UpdateColor", lightChange);
		foreach (GameObject o in GameObject.FindGameObjectsWithTag("Colored")) {
			o.SendMessage("UpdateColor", lightChange);
		}
	}

}

public struct LightChange {
	public string color1;
	public string color2;
	public float increment;
}
