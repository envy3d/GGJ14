/**
 * Code was borrowed from these devs:
 * 
 * STUFF DOWNLOADED FROM http://wiki.unity3d.com/index.php/Hermite_Spline_Controller
 * AUTHOR: Benoit FOULETIER (http://wiki.unity3d.com/index.php/User:Benblo)
 * MODIFIED BY F. Montorsi
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollower : MonoBehaviour {
	public GameObject pathRoot;
	public float pathSegmentDuration;
	public Transform player;
	public float playerRadiusSquared;

	private Transform[] nodes;
	private int destNodeIdx;
	private Vector3 lastImportantPosition;
	private Vector3 currPosition;
	private float lerpTimer;
	private bool loopAndIgnore;

	
	void Start () {
		PathInfo startingPathInfo;
		startingPathInfo.pathRoot = pathRoot;
		startingPathInfo.last = false;
		startingPathInfo.selectSpecificNode = false;
		startingPathInfo.specificNodeIndex = 0;
		startingPathInfo.loopAndIgnorePlayer = false;
		SetStartPath(startingPathInfo);
	}

	void Update () {
		if (currPosition == nodes[destNodeIdx].position) {
			lastImportantPosition = currPosition;
			lerpTimer = 0;
			if (loopAndIgnore) {
				destNodeIdx = destNodeIdx + 1;
				if (destNodeIdx == nodes.Length)
					destNodeIdx = 0;
			}
		}
		lerpTimer += Time.deltaTime * (1/pathSegmentDuration);
		int lastDestNodeIdx = destNodeIdx;
		if (!loopAndIgnore) {
			FindFurthestNodeTouchingPlayer();
		}
		if (lastDestNodeIdx != destNodeIdx) {
			lerpTimer = 0;
			lastImportantPosition = currPosition;
		}
		currPosition = Vector3.Lerp(lastImportantPosition, nodes[destNodeIdx].position, lerpTimer);
		transform.position = currPosition;
	}

	public void SetPath(PathInfo newPath) {
		if (pathRoot.Equals(newPath.pathRoot)) {
			return;
		}
		loopAndIgnore = newPath.loopAndIgnorePlayer;
		pathRoot = newPath.pathRoot;
		GetPathNodes();
		int currNodeIdx = newPath.last ? nodes.Length - 1 : 0;
		if (newPath.selectSpecificNode && (newPath.specificNodeIndex >= 0 && newPath.specificNodeIndex < nodes.Length)) {
			currNodeIdx = newPath.specificNodeIndex;
		}
		//currPosition = nodes[currNodeIdx].position;
		lastImportantPosition = currPosition;//nodes[currNodeIdx].position;
		destNodeIdx = currNodeIdx;
	}

	public void SetStartPath(PathInfo newPath) {
		loopAndIgnore = newPath.loopAndIgnorePlayer;
		pathRoot = newPath.pathRoot;
		GetPathNodes();
		int currNodeIdx = newPath.last ? nodes.Length - 1 : 0;
		if (newPath.selectSpecificNode && (newPath.specificNodeIndex >= 0 && newPath.specificNodeIndex < nodes.Length)) {
			currNodeIdx = newPath.specificNodeIndex;
		}
		//currPosition = nodes[currNodeIdx].position;
		lastImportantPosition = currPosition;//nodes[currNodeIdx].position;
		destNodeIdx = currNodeIdx;
	}

	private void FindFurthestNodeTouchingPlayer() {
		for (int idx = nodes.Length - 1; idx >= 0; --idx) {
			if (checkDistanceFromNode(idx)) {
				destNodeIdx = idx + 1;
				if (destNodeIdx == nodes.Length)
					destNodeIdx = nodes.Length - 1;
				break;
			}
		}
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

	private bool checkDistanceFromNode(int nodeIdx) {
		return (nodes[nodeIdx].position - player.position).sqrMagnitude < playerRadiusSquared ? true : false;
	}
	
}

public struct PathInfo {
	public GameObject pathRoot;
	public bool last;
	public bool selectSpecificNode;
	public int specificNodeIndex;
	public bool loopAndIgnorePlayer;
}
