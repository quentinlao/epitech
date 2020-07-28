using UnityEngine;
using System.Collections;

public class Explosion: MonoBehaviour
{
	public float explosionTime;
	public float explosionRadius;
	public float explosionPower;

    void Start ()
    {
		
		Destroy(this.gameObject, this.explosionTime);
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		
		foreach (Collider hit in colliders)
		{
			if (hit.rigidbody)
			{
				hit.rigidbody.AddExplosionForce(this.explosionPower, transform.position, explosionRadius);
                Vector3 closestPoint = hit.rigidbody.ClosestPointOnBounds(this.transform.position);
                float distance = Vector3.Distance(closestPoint, transform.position);
                float hitPoint = Mathf.Clamp01(distance / explosionRadius);
                if (hitPoint == 0)
                    hitPoint = explosionPower;
                else
                    hitPoint *= explosionPower;
            }
		}

		if (particleEmitter)
		{
			particleEmitter.emit = true;
            //yield return new WaitForSeconds(0.5f);
			//particleEmitter.emit = false;
		}
	}
}