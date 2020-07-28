using UnityEngine;
using System.Collections;

public class EraseObstacle : MonoBehaviour
{

    public Transform _target;

    Transform _transform;

    Collider _collide;

    Shader _ShadeTransparent;
    Shader _ShadeBumpedDiffuse;

    void Awake()
    {
        _transform = transform;
        _collide = null;
        _ShadeBumpedDiffuse = Shader.Find("Bumped Diffuse");
        _ShadeTransparent = Shader.Find("Transparent/Diffuse");
    }

	void LateUpdate ()
    {
	    RaycastHit hit;
        Debug.DrawRay(_transform.position, _target.position - _transform.position, Color.red);
        if (Physics.Raycast(_transform.position, _target.position - _transform.position, out hit))
        {
            if (hit.collider.tag == "Wall")
            {
                if (_collide != null && _collide.gameObject != hit.collider.gameObject)
                {
                    _collide.renderer.material.shader = _ShadeBumpedDiffuse;
                    _collide = hit.collider;
                }
                else
                {
                    hit.collider.renderer.material.shader = _ShadeTransparent;
                    _collide = hit.collider;
                }
            }
            else if (_collide != null)
            {
                _collide.renderer.material.shader = _ShadeBumpedDiffuse;
                _collide = null;
            }
        }
	}
}
