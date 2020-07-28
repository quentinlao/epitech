using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mob : MonoBehaviour {
	public int health = 100;
	public List<GameObject> WayPoints = new List<GameObject>();
	public float speed = 5;
	public Vector3 forwardVector = Vector3.forward;
    public Vector3 nextForwardVector = Vector3.forward;
    public CharacterController pcontroller;
    
	void Start()
	{
		transform.rotation = Quaternion.FromToRotation(forwardVector, WayPoints[0].transform.position - transform.position);
		Debug.Log(WayPoints[0].transform.position - transform.position);
       // pcontroller = gameObject.GetComponent("CharacterCont mob") as CharacterController;
	}

	void GetNextWaypoint()
	{
		Debug.Log("seeking next wp");
		if (WayPoints.Count > 0)
		{
			WayPoints.RemoveAt(0);
		}
		else
			Destroy(gameObject);
	}

	void Die()
	{
		Destroy(gameObject);
	}

	public void Update()
	{
		if (WayPoints.Count <= 0)
			GetNextWaypoint();
		else
		{
			if (Vector3.Distance(WayPoints[0].transform.position, transform.position) <= 0.3)
				GetNextWaypoint();
            
            transform.LookAt(WayPoints[0].transform.position);
            pcontroller.Move(Vector3.Normalize(WayPoints[0].transform.position - transform.position) * speed * Time.deltaTime);
		   	if (health <= 0)
				Die();
		}
    
        /*
            var hit : RaycastHit;
         
        if(Physics.Raycast(transform.position, transform.forward, hit, rayCastLength))
        {
                if(hit.collider.gameObject.tag == "Door")
                {
                    hit.collider.gameObject.animation.Play("Door_Open");
                }
        }
         */
	}
}
