using UnityEngine;
using System.Collections;

public class RotationElem : MonoBehaviour {

	public float rotate = 5;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.Rotate(new Vector3(0, 0, rotate) * Time.deltaTime);
	}
}
