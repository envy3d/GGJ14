using UnityEngine;
using System.Collections;

public class SceneLightColorChanger : MonoBehaviour {

	public string colorTag;
	public float colorChangeTime;

	private Light light;
	private float maxIntensity;
	private float destIntensity;
	private float prevDestIntensity;
	private float lerpTimer;

	// Use this for initialization
	void Start () {
		light = transform.gameObject.GetComponent<Light>();
		maxIntensity = light.intensity;
		destIntensity = maxIntensity;
		prevDestIntensity = destIntensity;
		lerpTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (destIntensity != light.intensity) {
			lerpTimer += Time.deltaTime * (1/colorChangeTime);
			light.intensity = Mathf.Lerp(prevDestIntensity, destIntensity, lerpTimer);
		}
	}

	public void UpdateColor(LightChange lightChange) {
		lerpTimer = 0;

		if (lightChange.color1.Equals(colorTag) && lightChange.color2.Equals(colorTag)) {
			prevDestIntensity = destIntensity;
			destIntensity = maxIntensity;
		}
		else if (lightChange.color1.Equals(colorTag)) {
			prevDestIntensity = destIntensity;
			destIntensity = maxIntensity * (1 - lightChange.increment);
		}
		else if (lightChange.color2.Equals(colorTag)) {
			prevDestIntensity = destIntensity;
			destIntensity = maxIntensity * lightChange.increment;
		}
		else {
			prevDestIntensity = destIntensity;
			destIntensity = 0f;
		}
	}

}
