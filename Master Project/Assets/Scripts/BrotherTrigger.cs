using UnityEngine;
using System.Collections;

public class BrotherTrigger : MonoBehaviour {

	public GameObject pathRoot;
	public bool attachToLastNode = false;
	public bool loopAndIgnorePlayer = false;
	public bool selectSpecificNode = false;
	public int specificNodeIndex = 0;

	public PathFollower pathFollower;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player")
		{
			PathInfo pathInfo;
			pathInfo.pathRoot = pathRoot;
			pathInfo.last = attachToLastNode;
			pathInfo.selectSpecificNode = selectSpecificNode;
			pathInfo.specificNodeIndex = specificNodeIndex;
			pathInfo.loopAndIgnorePlayer = loopAndIgnorePlayer;
			pathFollower.SetPath(pathInfo);
		}
	}
}
