using UnityEngine;
using System.Collections;

public class button2 : MonoBehaviour {
	
	void OnMouseDown ()
	{
		print (renderer.material.color.a);
		if (renderer.material.color.a > 0.1f) 
		{
			AutoFade.LoadLevel("Lvl_test_2", 1, 1, Color.black);
		}
	}
}