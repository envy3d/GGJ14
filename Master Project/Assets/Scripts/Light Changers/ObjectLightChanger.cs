using UnityEngine;
using System.Collections;

public class ObjectLightChanger : MonoBehaviour {

	public string colorTag;
	public float colorChangeTime;

	public Color coldColor, mediumColor, hotColor;

	private float currR, currG, currB;
	private float destR, destG, destB;
	private float prevDestR, prevDestG, prevDestB;
	
	private float currAlpha;
	private float destAlpha;
	private float prevDestAlpha;
	private float alphaLerpTimer, colorLerpTimer;

	// Use this for initialization
	void Start() {

		switch (colorTag)
		{
			case "cold":
				currR = coldColor.r;
				currG = coldColor.g;
				currB = coldColor.b;
			break;

			case "medium":
				currR = mediumColor.r;
				currG = mediumColor.g;
				currB = mediumColor.b;
			break;

			case "hot":
				currR = hotColor.r;
				currG = hotColor.g;
				currB = hotColor.b;
			break;
		}

		currAlpha = 1;

		destAlpha = currAlpha;
		prevDestAlpha = destAlpha;

		destR = currR;
		destG = currG;
		destB = currB;

		prevDestR = destR;
		prevDestG = destG;
		prevDestB = destB;

		transform.gameObject.renderer.material.SetColor("_Color", new Color(currR, currG, currB, currAlpha));

		alphaLerpTimer = colorLerpTimer = 0;
	}
	
	// Update is called once per frame
	void Update() {

		if (destR != currR || destG != currG || destB != currB)
		{
			colorLerpTimer += Time.deltaTime * (1/colorChangeTime);

			currR = Mathf.Lerp (prevDestR, destR, colorLerpTimer);
			currG = Mathf.Lerp (prevDestG, destG, colorLerpTimer);
			currB = Mathf.Lerp (prevDestB, destB, colorLerpTimer);

			transform.gameObject.renderer.material.SetColor("_Color", new Color(currR, currG, currB, currAlpha));
		}

		if (destAlpha != currAlpha) {
			transform.gameObject.renderer.enabled = true;
			alphaLerpTimer += Time.deltaTime * (1/colorChangeTime);
			currAlpha = Mathf.Lerp(prevDestAlpha, destAlpha, alphaLerpTimer);
			Color color = transform.gameObject.renderer.material.color;
			color.a = currAlpha;
			transform.gameObject.renderer.material.SetColor("_Color", color);
			if (currAlpha == 0) {
				transform.gameObject.renderer.enabled = false;
			}
		}
	}

	public void changeColorToRandom()
	{
		string newTag = colorTag;

		while (newTag == colorTag)
		{
			switch (Random.Range (0,3))
			{
				case 0:
					newTag = "cold";
				break;

				case 1:
					newTag = "medium";
				break;

				case 2:
					newTag = "hot";
				break;
			}
		}

		colorTag = newTag;

		prevDestR = currR;
		prevDestG = currG;
		prevDestB = currB;
		
		switch (newTag)
		{
			case "cold":
				
				destR = coldColor.r;
				destG = coldColor.g;
				destB = coldColor.b;

			break;

			case "medium":
				destR = mediumColor.r;
				destG = mediumColor.g;
				destB = mediumColor.b;
			
			break;

			case "hot":

				destR = hotColor.r;
				destG = hotColor.g;
				destB = hotColor.b;
			
			break;
		}
	}


	public void UpdateColor(LightChange lightChange) {
		alphaLerpTimer = colorLerpTimer = 0;
		
		if (lightChange.color1.Equals(colorTag) && lightChange.color2.Equals(colorTag)) {
			prevDestAlpha = currAlpha;
			destAlpha = 1f;
		}
		else if (lightChange.color1.Equals(colorTag)) {
			prevDestAlpha = currAlpha;
			destAlpha = 1 - lightChange.increment;
		}
		else if (lightChange.color2.Equals(colorTag)) {
			prevDestAlpha = currAlpha;
			destAlpha = lightChange.increment;
		}
		else {
			prevDestAlpha = currAlpha;
			destAlpha = 0f;
		}
	}

}
