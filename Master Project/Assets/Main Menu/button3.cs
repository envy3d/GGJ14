using UnityEngine;
using System.Collections;

public class button3 : MonoBehaviour {
	
	void OnMouseDown ()
	{
		print (renderer.material.color.a);
		if (renderer.material.color.a > 0.1f) 
		{
			Application.Quit ();
		}
	}
}