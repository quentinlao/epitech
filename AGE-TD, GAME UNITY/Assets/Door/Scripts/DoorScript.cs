using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

    public GameObject MovableDoorPart;
    public GameObject DoorPart;

    public GameObject Target;

    public float speed = 0.02f;

    private bool move = false;

    private float distanceMax;
    private Vector3 TargetPosition;
    private Vector3 openPosition;
    private Vector3 closePosition;
    private Vector3 dir;
    private Vector3 pas;

    public void OnTriggerEnter(Collider col)
    {
        move = true;
        pas = dir;
        TargetPosition = openPosition;
    }

    public void OnTriggerExit(Collider col)
    {
        move = true;
        pas = -dir;
        TargetPosition = closePosition;
    }

    // Use this for initialization
    void Start()
    {
        openPosition = Target.transform.position;
        closePosition = MovableDoorPart.transform.position;
        distanceMax = Vector3.Distance(closePosition, openPosition) + 0.01f;
        dir = openPosition - closePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (Vector3.Distance(closePosition, MovableDoorPart.transform.position) +
                Vector3.Distance(openPosition, MovableDoorPart.transform.position) > distanceMax)
            {
                move = false;
                MovableDoorPart.transform.position = TargetPosition;
            }
            else
            {
                MovableDoorPart.transform.position += pas * speed * Time.deltaTime;
            }
        }
    }
}
