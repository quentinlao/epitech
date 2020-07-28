using UnityEngine;
using System.Collections;

public class loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Application.LoadLevel(PlayerPrefs.GetString("SceneName"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
