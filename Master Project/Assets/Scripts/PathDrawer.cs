using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathDrawer : MonoBehaviour {

	public Color color;

	private Transform[] nodes;
	private GameObject pathRoot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void GetPathNodes() {

		if (pathRoot == null)
			nodes = null;
		
		List<Component> components = new List<Component>(pathRoot.GetComponentsInChildren(typeof(Transform)));
		
		List<Transform> transforms = components.ConvertAll(c => (Transform)c);
		
		transforms.Remove(pathRoot.transform);
		transforms.Sort(delegate(Transform a, Transform b)
		{
			return a.name.CompareTo(b.name);
		});
		
		nodes = transforms.ToArray();
	}
	
	void OnDrawGizmos()
	{
		pathRoot = transform.gameObject;
		GetPathNodes();
		if (nodes.Length < 2)
			return;
		
		Vector3 prevPos = nodes[0].position;

		Color tempColor = new Color(color.r, color.g, color.b);
		Gizmos.color = tempColor;
		for (int nodeIdx = 1; nodeIdx < nodes.Length; nodeIdx++)
		{
			Vector3 currPos = nodes[nodeIdx].position;
			Gizmos.DrawLine(prevPos, currPos);
			prevPos = currPos;
		}
		tempColor = new Color(color.r/2, color.g/2, color.b/2);
		for (int nodeIdx = 0; nodeIdx < nodes.Length; nodeIdx++)
		{
			Gizmos.DrawSphere(nodes[nodeIdx].position, 0.1f);
		}
	}
}
