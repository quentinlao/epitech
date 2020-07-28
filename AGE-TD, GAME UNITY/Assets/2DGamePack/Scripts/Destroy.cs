using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour
{

    public float _timeBeforeDestroy;
    public float explosionRadius;
    public float explosionPower;
    public GameObject _destroyEffect;

    private float _totalTime;

    void Start()
    {
        _totalTime = 0;
    }

    void OnCollisionEnter()
    {
        DestroyProp();
    }

    void Update()
    {
        _totalTime += Time.deltaTime;

        if (_totalTime > _timeBeforeDestroy)
            DestroyProp();
    }

    private void DestroyProp()
    {
        if (_destroyEffect != null)
            Instantiate(_destroyEffect, transform.position, transform.rotation);
        if (_destroyEffect.tag == "explosion")
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            //Apply force to all surrounding rigid bodies
            foreach (Collider hit in colliders)
            {
                if (hit.rigidbody)
                {
                    hit.rigidbody.AddExplosionForce(this.explosionPower,
                                                     transform.position,
                                                     explosionRadius);
                    Vector3 closestPoint = hit.rigidbody.ClosestPointOnBounds(this.transform.position);
                    float distance = Vector3.Distance(closestPoint, transform.position);
                    float hitPoint = Mathf.Clamp01(distance / explosionRadius);
                    if (hitPoint == 0)
                        hitPoint = explosionPower;
                    else
                        hitPoint *= explosionPower;
                    // Tell the rigidbody or any other script attached to the hit object
                    // how much damage is to be applied!
                    hit.rigidbody.SendMessageUpwards("ApplyDamage", hitPoint,
                                                      SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        Destroy(gameObject);
    }
}
