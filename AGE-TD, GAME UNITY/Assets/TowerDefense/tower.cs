using UnityEngine;
using System.Collections;

public class tower : MonoBehaviour {
	public bool isActive = true;
	public int level = 1;
	public int health = 100;
	/// <summary>
	/// fire rate in seconds
	/// </summary>
	public float fireRate = 1.0f;
	/// <summary>
	/// Set the tag of the gameobjects representing any enemy unit.
	/// </summary>
	public string EnemyTag = "enemy";
	public GameObject shotPrefab;
	public float range = 10;
	public Vector3 launchOffset = Vector3.zero;
	public Vector3 forwardVector = Vector3.forward;
    public Vector3 positionOffset;
	void Start () {
		
	}

	float lastShot = 0;
	bool isShooting = false;
	GameObject target;

	void Aim()
	{
		float minDist = float.MaxValue;
		isShooting = false;
		target = null;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag(EnemyTag))
		{
			float dist = Vector3.Distance(go.transform.position, transform.position);

			if (dist < minDist)
			{
				target = go;
				minDist = dist;
				isShooting = true;
			}
		}
		if (minDist == float.MaxValue || minDist > range)
		{
			isShooting = false;
			target = null;
		}
	}

	void Fire()
	{
		if (target == null || !target.active)
			return;
		transform.rotation = Quaternion.FromToRotation(forwardVector, target.transform.position - transform.position);
		//dir = target.transform.position - transform.position;
		GameObject shot = Instantiate(shotPrefab, transform.position, transform.rotation) as GameObject;
		shot.transform.Translate(launchOffset);
		shot.GetComponent<bullet>().Target = target.transform.position;
		lastShot = 0;
	}

	void Update () {
		if (!isActive)
			return;
		if (health <= 0)
			Destroy(this);
		lastShot += Time.deltaTime;
		if (lastShot >= fireRate)
		{
			Aim();
			if (isShooting)
				Fire();
		}
	}
}
