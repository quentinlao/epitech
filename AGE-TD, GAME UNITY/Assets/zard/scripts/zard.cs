using UnityEngine;
using System.Collections;

public class zard : MonoBehaviour {
    public float life;
    public GameObject mesh;
    public ParticleEmitter die;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (this.life <= 0)
        {
            ParticleEmitter part = (ParticleEmitter)Instantiate(die, mesh.transform.position, mesh.transform.rotation);
            Destroy(mesh);
            Destroy(this.gameObject);
            Destroy(part.gameObject, 0.3f);
        }
	}

    void ApplyDamage(float dommage)
    {
        Debug.Log("je te pete moi");
        this.life -= dommage;
    }
}
