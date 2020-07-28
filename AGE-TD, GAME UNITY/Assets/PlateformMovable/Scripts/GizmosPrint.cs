using UnityEngine;
using System.Collections;

public class GizmosPrint : MonoBehaviour {

	public string IconName;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position, IconName);
	}
}
