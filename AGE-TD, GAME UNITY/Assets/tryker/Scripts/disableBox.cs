using UnityEngine;
using System.Collections;

public class disableBox : MonoBehaviour {

	public GameObject bouclier;
	public GameObject brasGauche;
	public GameObject brasDroit;
    public GameObject epee;
	// Use this for initialization
	void Start () {
		//Permet de ne pas afficher les box des bouclier et armes.
		bouclier.GetComponent<MeshRenderer>().enabled = false;
		brasGauche.GetComponent<MeshRenderer>().enabled = false;
		brasDroit.GetComponent<MeshRenderer>().enabled = false;
        epee.GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
