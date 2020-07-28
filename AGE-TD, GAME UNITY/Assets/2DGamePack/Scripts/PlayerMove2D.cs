using UnityEngine;
using System.Collections;
using System;

public class PlayerMove2D : MonoBehaviour
{
	public GUISkin _skin;
	
	public int _player = 1;
	
	public string _left;
	public string _right;
	public string _jump;
	
    public float _speed = 10.0F;
    public float _pushPower = 5.0F;
    public float _jumpForce = 50f;
    private float _jumpSpeed = 10f;
    public float _gravity = -30f;
    public float _air = 6f;
    private int _rotationForce = 20;
    public float _rotationSpeed = 5f;

    public float life;
    public float armor;
    public float maxlife;
    public float maxArmor;

    private float _vAxis;
    private float _hAxis;
    private bool _jumping = false;
    private float _verticalSpeed = 0;

    private CharacterController _CharacterController;
    private Transform _Transform;
    public void Awake()
    {
        _CharacterController = GetComponent<CharacterController>();
        _Transform = transform;

        foreach (AnimationState state in animation)
            state.speed = _speed * 0.3F;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_jumping ||
            (hit.collider.tag != "Pushable" || hit.moveDirection.y < -0.3))
            return;

        Rigidbody hitBody = hit.collider.attachedRigidbody;
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);

        hitBody.velocity = (_pushPower - hitBody.mass) * pushDir;
    }

    public void Update()
    {
        if (life <= 0)
            Debug.Log("DIE!!!");
        GetInputs();
        SetAnimations();
        DeterminateVerticalSpeed();
        SmoothMove();
        SmoothFall();
        SmoothRotate();
		IsInScreen();
    }
	
	private void IsInScreen()
	{
		Vector3 screen = Camera.mainCamera.WorldToScreenPoint(transform.position);
		if (screen.x < 0 || screen.x > Screen.width ||
			screen.y < 0 || screen.y > Screen.height)
			life = 0;
	}
	
    private void GetInputs()
    {
		if (Input.GetKey (_left))
			_hAxis = -1;
		else if (Input.GetKey (_right))
			_hAxis = 1;
		else 
			_hAxis = 0;
		
    }

    private void SetAnimations()
    {
        if (_hAxis == 0 && !animation.Play("idle"))
            animation.Play("idle");
        if ((_hAxis < -0.6 || _hAxis > 0.6) && !animation.IsPlaying("walk"))
            animation.Play("walk");
        else if (_hAxis >= -0.6 && _hAxis <= 0.6 && !animation.IsPlaying("walk"))
            animation.Play("walk");
    }

    private void DeterminateVerticalSpeed()
    {
        if (Input.GetButtonDown(_jump) &&
            _CharacterController.isGrounded)
        {
            _jumping = true;
            _verticalSpeed = _jumpForce;
        }

        if (_verticalSpeed < _air)
            _jumping = false;

        if (_jumping)
            _verticalSpeed = Mathf.SmoothStep(_verticalSpeed, 0, _jumpSpeed * Time.deltaTime);
        else
            _verticalSpeed = Mathf.SmoothStep(_verticalSpeed, _gravity, _jumpSpeed * Time.deltaTime);
    }

    private void SmoothFall()
    {
        Vector3 vertical = new Vector3(0, 1, 0);
        vertical.y = 0;

        Vector3 movement = (vertical * _vAxis);
        movement *= _speed;
        movement.y = _verticalSpeed;
     
        _CharacterController.Move(movement * Time.deltaTime);
    }

    private void SmoothMove()
    {
        Vector3 translate = new Vector3(_speed * _hAxis, 0, 0);

        _CharacterController.Move(translate * Time.deltaTime);
    }

    private void SmoothRotate()
    {
        float rot = 0;

        if (_hAxis > 0)
            rot = 90;
        else if (_hAxis < 0)
            rot = 270;

        if (rot != 0)
        {
            Quaternion targetRotation = Quaternion.Euler(_Transform.eulerAngles.x, rot, _Transform.eulerAngles.z);
            _Transform.rotation = Quaternion.Lerp(_Transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    void AddLife(int nbLife)
    {
        this.life += nbLife;
        if (this.life > this.maxlife) this.life = this.maxlife;
    }

    void AddArmor(int nbArmor)
    {
        this.armor += nbArmor;
        if (this.armor > this.maxArmor) this.armor = this.maxArmor;
    }

    void ApplyDamage(float dommage)
    {
        this.life -= (int)dommage;
    }
	
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Shoot")
		{
			ApplyDamage (1);	
		}
	}
	
	void OnGUI()
	{
		GUI.skin = _skin;
		if (life <= 0)
		{
			GUI.TextArea(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), "Player " + (_player == 1 ? "2" : "1") + "WIN !");
		}
	}
}
