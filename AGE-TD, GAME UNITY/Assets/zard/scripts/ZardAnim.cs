using UnityEngine;
using System.Collections;

//Le component Rigidbody est obligatoire
[RequireComponent(typeof(Rigidbody))]
//Le component CapsuleCollider est obligatoire
[RequireComponent(typeof(CapsuleCollider))]

public class ZardAnim : MonoBehaviour {

    public GameObject mesh; //le modele a animer et a faire avancer
    public float coefSpeed; //Vvaleur qui permet de corriger la vitesse d'animation

	// A utiliser pour l'initialisation des variables
	void Start () {
	}
	
	// Update est appele a chaque frames
	void Update () {
        //permet de faire avancer le modele
        this.rigidbody.AddRelativeForce(0, 0, 35);
        //calcule la vitesse horizontal du modele
        float speed = Mathf.Abs(rigidbody.velocity.x) + Mathf.Abs(rigidbody.velocity.z);

        if (speed > 0.3)
        {
            //si le modele avance assez vite on adapte la vitesse de son animation "walk"
            //a la vitesse de son deplacement
            this.mesh.animation["walk"].speed = speed * coefSpeed;
            //calcule de l'angle du vecteur de deplacement
            float angle = Mathf.Atan2(rigidbody.velocity.x, rigidbody.velocity.z) * Mathf.Rad2Deg;
            //orientation du modele par rapport a la direction de son deplacement
            transform.eulerAngles = new Vector3(0, angle, 0);
        }
        else
        {
            //si le modele n'avance plus assez, il passe en idle.
            this.mesh.animation["walk"].speed = 0; // pas de idle donc le speed de l'anim est mis a 0
        }
	}
}
