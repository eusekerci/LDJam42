using System;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{

	private Vector2 _direction;
	private float _speed;
	private bool _isInitiliazed;
	private Transform _transform;

	private void Awake()
	{
		_isInitiliazed = false;
	}

	private void Start()
	{
		Init();
	}
	
	private void Init()
	{
		_direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
		_speed = 15f;
		_transform = GetComponent<Transform>();
		_isInitiliazed = true;
	}

	private void FixedUpdate()
	{
		if (!_isInitiliazed)
		{
			return;
		}

		_transform.position += (Vector3)(_direction * Time.fixedDeltaTime * _speed);

		if (_transform.position.magnitude > 15f)
		{
			ChangeDirection();
		} 
	}

	private void ChangeDirection()
	{
		ChangeDirection(Vector2.zero);
	}
	
	private void ChangeDirection(Vector2 _modifier)
	{
		Vector2 normal = (Vector2) _transform.position.normalized;
		_direction = 2 * (Vector2.Dot(_direction, normal)) * normal - _direction;
		_direction = (-1 * _direction + _modifier.normalized).normalized;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Post")
		{
			ChangeDirection();
		}
		else if (other.tag == "Player")
		{
			ChangeDirection(_transform.position.normalized - other.transform.position.normalized);
		}
	}
}
