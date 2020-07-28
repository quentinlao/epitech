using UnityEngine;
using System.Collections;

public class Follow2D : MonoBehaviour
{

    public Transform _Player;
    public float _cameraSpeed;

    Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

	void Update ()
    {
        Vector3 newPos = _transform.position;
        newPos.x = Mathf.SmoothStep(newPos.x, _Player.position.x, _cameraSpeed * Time.deltaTime);

        _transform.position = newPos;
	}
}
