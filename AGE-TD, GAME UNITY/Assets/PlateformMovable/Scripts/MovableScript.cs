using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovableScript : MonoBehaviour
{
    public GameObject EndItem;

    public bool Loop
    {
        get { return loop; }
        set
        {
            if (loop != value)
            {
                loop = value;
                if (loop)
                    stop = false;
            }
        }
    }

	public bool StartOnTrigger = false;
	
    public bool loop = true;

    public bool stop = true;

    private float distanceMax;
    private Vector3 pas;
    private Vector3 start;

	private List<GameObject> m_objs;
	
    /// <summary>
    /// Vitesse entre 0 && 1
    /// </summary>
    public float speed;

    // Use this for initialization
    void Start()
    {

        pas = EndItem.transform.position - this.transform.position;
        transform.position = this.transform.position;
        start = this.transform.position;
        distanceMax = Vector3.Distance(this.transform.position, EndItem.transform.position) + 0.01f;
        if (StartOnTrigger)
			stop = true;
		
		m_objs = new List<GameObject>();
    }

	void OnTriggerEnter(Collider other)
	{
		if (StartOnTrigger)
			stop = false;
		if (!m_objs.Contains(other.gameObject))
			m_objs.Add(other.gameObject);
	}

	void OnTriggerExit(Collider other)
	{
		if (m_objs.Contains(other.gameObject))
			m_objs.Remove(other.gameObject);
	}
	
    // Update is called once per frame
    void Update()
    {
        if (stop)
            return;
        if (Vector3.Distance(transform.position, start) +
            Vector3.Distance(transform.position, EndItem.transform.position) > distanceMax)
        {
            pas = -pas;
            if (!Loop || StartOnTrigger)
                stop = true;
        }
		Vector3 _step = pas * speed * Time.deltaTime;
        transform.position += _step;
		foreach (GameObject elem in m_objs) {
			elem.transform.position += _step;
		}
    }
}
