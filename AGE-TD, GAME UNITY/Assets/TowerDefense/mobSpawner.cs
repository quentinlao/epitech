using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mobSpawner : MonoBehaviour {
	public List<GameObject> WayPoints = new List<GameObject>();
	public int numberBySpawm = 5;
	public float intervalBetweenSpawn = 10;
	public GameObject mobPrefab;
	public string unitsTag = "enemy";

	void Start () {
		
	}

	void Spawn()
	{
		nbSpawned++;
		GameObject go = Instantiate(mobPrefab, transform.position, transform.rotation) as GameObject;
		go.GetComponent<mob>().WayPoints = new List<GameObject>();
		go.tag = unitsTag;
		foreach (GameObject wp in WayPoints)
			go.GetComponent<mob>().WayPoints.Add(wp);
	}

	bool isSpawning = false;
	float nbSpawned = 0;
	float lastSpawn = float.MaxValue;
	void Update () {
		lastSpawn += Time.deltaTime;
		if (lastSpawn >= intervalBetweenSpawn)
		{
			isSpawning = true;
			lastSpawn = 0;
		}
		if (isSpawning)
		{
			Spawn();
			if (nbSpawned == numberBySpawm)
			{
				isSpawning = false;
				nbSpawned = 0;
			}
		}
	}
}
