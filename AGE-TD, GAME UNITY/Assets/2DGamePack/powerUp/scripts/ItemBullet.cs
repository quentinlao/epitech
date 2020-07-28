using UnityEngine;
using System.Collections;

public class ItemBullet : MonoBehaviour {

    public int nbBoom;

    // Use this for initialization
    void Start()
    {
        if (particleEmitter)
            particleEmitter.emit = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            other.SendMessageUpwards("AddBoom", this.nbBoom);
            if (renderer)
                renderer.enabled = false;
            if (particleEmitter)
                particleEmitter.emit = true;
            else
                Destroy(gameObject);
        }
    }
}
