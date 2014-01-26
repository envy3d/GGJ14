using UnityEngine;
using System.Collections;

public class button1 : MonoBehaviour {
	
	void OnMouseDown ()
	{
		print (renderer.material.color.a);
		if (renderer.material.color.a > 0.1f) 
		{
			AutoFade.LoadLevel("tutorial", 1, 1, Color.black);
		}
	}
}