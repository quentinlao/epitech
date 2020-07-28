using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	public Vector3 target;
	public float speed;
	public float radius = 2;
	public GameObject explosionPrefab;
	public Vector3 forwardAssetVector = Vector3.forward;
	public int damages = 10;

	public Vector3	 Target
	{
		get { return target; }
		set { target = value; }
	}

	void Start () {
		transform.rotation = Quaternion.FromToRotation(forwardAssetVector, target - transform.position);
	}

	void Touched()
	{

		foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
		{
			if (col.gameObject.GetComponentInChildren<mob>() != null)
			{

				col.gameObject.GetComponentInChildren<mob>().health -= damages;
			}
		}
		if (explosionPrefab != null)
			Instantiate(explosionPrefab, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	void Update () {
		if (Vector3.Distance(target, transform.position) < 0.1f)
			Touched();
		transform.rotation = Quaternion.FromToRotation(forwardAssetVector, target - transform.position);
		transform.position += (Vector3.Normalize(target - transform.position) * Time.deltaTime * speed);
	}
}
