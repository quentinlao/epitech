using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	
	public enum Way { Up = 1, Right = 2 };
	public Way _way;
	public float _speed = 5.0f;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_way == Way.Up)
		{
			gameObject.gameObject.transform.Translate(0, _speed * Time.deltaTime, 0);	
		}
		else
		{
			gameObject.gameObject.transform.Translate(_speed * Time.deltaTime, 0, 0);	
		}
	}
}
