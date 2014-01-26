using UnityEngine;
using System.Collections;

public class ReturnTuto : MonoBehaviour {

	void OnMouseDown ()
	{
		print (renderer.material.color.a);
		if (renderer.material.color.a > 0.1f) 
		{
			AutoFade.LoadLevel("mainmenu", 1, 1, Color.black);
		}
	}
}