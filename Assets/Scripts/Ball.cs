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
	private PlayerManager _playerManager;
	private PostAllignment _postAllignment;
	private bool _isInvincible;
	private float _invincibleTime = 0.5f;

	private void Awake()
	{
		_isInitiliazed = false;
	}
	
	public void Init(PlayerManager pm, PostAllignment pa)
	{
		_playerManager = pm;
		_postAllignment = pa;
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
		float angle = Utils.Vector2Extension.GetRadiant(_transform.position.normalized);
		int minIndex = -1;
		int maxIndex = -1;
		float minValue = float.MaxValue;
		float maxValue = float.MinValue;
			
		for (int j = 0; j < posts.Count; j++)
		{
			float postAngle = Utils.Vector2Extension.GetRadiant(posts[j].position.normalized);
			if ((angle - postAngle < minValue) && (angle - postAngle > 0))
			{
				minIndex = j;
				minValue = angle - postAngle;
			}
			else if (postAngle > maxValue)
			{
				maxValue = postAngle;
				maxIndex = j;
			}
		}

		_playerManager.KillPlayer(minIndex != -1 ? minIndex : maxIndex);
	}

	private void ChangeDirection()
	{
		ChangeDirection(Vector2.zero);
	}
	
	private void ChangeDirection(Vector2 modifier)
	{
		Vector2 normal = (Vector2) _transform.position.normalized;
		_direction = 2 * (Vector2.Dot(_direction, normal)) * normal - _direction;
		_direction = (-1 * _direction).normalized;
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
