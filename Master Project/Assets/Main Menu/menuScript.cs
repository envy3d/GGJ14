using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {

	public GameObject play, howToPlay, quit;

	private float playAlpha = 0, howToPlayAlpha = 0, quitAlpha = 0;

	private const float selectThreshold = 0.66f;

	void refreshAlpha () {
		playAlpha = play.renderer.material.GetColor ("_Color").a;
		howToPlayAlpha = howToPlay.renderer.material.GetColor ("_Color").a;
		quitAlpha = quit.renderer.material.GetColor ("_Color").a;
	}
	
	void Start () {
		refreshAlpha ();
	}

	// Update is called once per frame
	void Update () {
		refreshAlpha ();

		if (Input.GetMouseButtonDown (0)) //42, 39, 54
		{
			if (playAlpha >= selectThreshold)
				AutoFade.LoadLevel ("Lvl_test_2", 1, 1, new Color(0.165f, 0.153f, 0.212f));
			else if (howToPlayAlpha >= selectThreshold)
				print ("How to Play selected!");
			else if (quitAlpha >= selectThreshold)
				Application.Quit ();
		}
	}
}
