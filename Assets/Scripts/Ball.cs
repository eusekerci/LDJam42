using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{

	private Vector2 _direction;
	private float _speed;
	private bool _isInitiliazed;
	private Transform _transform;
	[SerializeField] private PlayerManager _playerManager;
	[SerializeField] private PostAllignment _postAllignment;
	private bool _isInvincible;
	private float _invincibleTime = 0.5f;

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
		_isInvincible = false;
		_direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
		_speed = 15f;
		_transform = GetComponent<Transform>();
		_isInitiliazed = true;
	}

	private void FixedUpdate()
	{
		if (!_isInitiliazed)
			return;

		_transform.position += (Vector3)(_direction * Time.fixedDeltaTime * _speed);

		if (_transform.position.magnitude > 15f && !_isInvincible)
		{
			ChangeDirection();
			StartCoroutine(InvincibleCoroutine(_invincibleTime));
			KillPlayer();
		}
	}

	private void KillPlayer()
	{
		List<Transform> posts = _postAllignment.GetPostTransforms();
		float angle = Utils.Vector2Extension.GetAngle((Vector2) _transform.position.normalized);
		int min = 0;
		float minValue = 361;
		for (int j = 0; j < posts.Count; j++)
		{
			float postAngle = Utils.Vector2Extension.GetAngle(posts[j].position.normalized);
			if ((angle - postAngle < minValue) && (angle - postAngle > 0))
			{
				min = j;
				minValue = angle - postAngle;
			}
		}

		_playerManager.KillPlayer(min);
	}

	private void ChangeDirection()
	{
		ChangeDirection(Vector2.zero);
	}
	
	private void ChangeDirection(Vector2 modifier)
	{
		Vector2 normal = (Vector2) _transform.position.normalized;
		_direction = 2 * (Vector2.Dot(_direction, normal)) * normal - _direction;
		_direction = (-1 * _direction + modifier.normalized).normalized;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_isInvincible)
			return;
		if (other.CompareTag("Post"))
		{
			ChangeDirection();
		}
		else if (other.CompareTag("Player"))
		{
			ChangeDirection(_transform.position.normalized - other.transform.position.normalized);
		}
	}

	private IEnumerator InvincibleCoroutine(float t)
	{
		_isInvincible = true;
		yield return new WaitForSeconds(t);
		_isInvincible = false;

	}
}
