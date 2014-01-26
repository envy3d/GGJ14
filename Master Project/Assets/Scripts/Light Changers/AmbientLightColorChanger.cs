using UnityEngine;
using System.Collections;

public class AmbientLightColorChanger : MonoBehaviour {

	public float colorChangeTime;
	public Color[] ambientLightColors;
	public string[] colorTags;

	private int currColorIndex;
	private int prevColorIndex;
	private Color destColor;
	private Color prevColor;
	private float lerpTimer;

	// Use this for initialization
	void Start() {
		currColorIndex = 0;
		prevColorIndex = 0;
		destColor = ambientLightColors[currColorIndex];
		prevColor = ambientLightColors[currColorIndex];
		lerpTimer = 0;
	}
	
	// Update is called once per frame
	void Update() {
		if (lerpTimer < 1) {
			lerpTimer += Time.deltaTime * (1/colorChangeTime);
			RenderSettings.ambientLight = Color.Lerp(prevColor, destColor, lerpTimer);
		}
	}

	public void UpdateColor(LightChange lightChange) {
		lerpTimer = 0;

		for (int i = 0, len = colorTags.Length; i < len; i++) {
			if (colorTags[i].Equals(lightChange.color1)) {
				prevColorIndex = i;
			}
			if (colorTags[i].Equals (lightChange.color2)) {
				currColorIndex = i;
			}
		}

		if (prevColorIndex == currColorIndex) {
			prevColor = RenderSettings.ambientLight;
			destColor = ambientLightColors[currColorIndex];
		}
		else {
			prevColor = RenderSettings.ambientLight;
			destColor = Color.Lerp(ambientLightColors[prevColorIndex], ambientLightColors[currColorIndex], lightChange.increment);
		}
	}
}