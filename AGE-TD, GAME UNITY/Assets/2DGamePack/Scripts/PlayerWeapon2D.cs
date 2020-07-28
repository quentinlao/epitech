using UnityEngine;
using System.Collections;

public class PlayerWeapon2D : MonoBehaviour
{
	public string _fire;
	
    public float boom;
    public float maxBoom;

    public float _LaserSpeed;
    public float _MortarAbsSpeed;
    public float _MortarOrdSpeed;
    public GameObject _Laser;
    public GameObject _Mortar;

    private GameObject _player;
    private Transform _transform;
    void Awake()
    {
        _transform = transform;
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Input.GetButtonDown(_fire))
            FireLaser();
        if (Input.GetButtonDown("Fire2"))
            FireMortar();
    }

    private void FireLaser()
    {
        Vector3 posFire = new Vector3(_transform.position.x, _transform.position.y + 0.8F, _transform.position.z);

        GameObject instantiatedProjectile = (GameObject)Instantiate(_Laser, posFire, _Laser.transform.rotation);
        instantiatedProjectile.rigidbody.velocity = new Vector3(_LaserSpeed * Mathf.Sign(_transform.forward.x), 0, 0);

        Physics.IgnoreCollision(instantiatedProjectile.collider, _transform.root.collider);
    }

    private void FireMortar()
    {
        if (boom > 0)
        {
            Vector3 posFire = new Vector3(_transform.position.x, _transform.position.y + 1.3F, _transform.position.z);

            boom = boom - 1;
            GameObject instantiatedProjectile = (GameObject)Instantiate(_Mortar, posFire, _transform.rotation);
            instantiatedProjectile.rigidbody.velocity = new Vector3(_MortarAbsSpeed * Mathf.Sign(_transform.forward.x), _MortarOrdSpeed, 0);

            Physics.IgnoreCollision(instantiatedProjectile.collider, transform.root.collider);
        }
    }

    void AddBoom(int nbBoom)
    {
        this.boom += nbBoom;
        if (this.boom > this.maxBoom) this.boom = this.maxBoom;
        Debug.Log("addBoom" + this.boom.ToString());
    }
}
