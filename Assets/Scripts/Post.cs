﻿using UnityEngine;

public class Post : MonoBehaviour {

	private int _segments = 50;
	private Vector2 _position;
	private Vector2 _radius;
	private float _angle;
	private LineRenderer _line;
	private bool _isInitialized;
	private Player _player;
	
	private void Awake ()
	{
		_isInitialized = false;
	}

	public void Init(Player player)
	{
		_player = player;
		_radius.x = 15;
		_radius.y = 15;
		_line = gameObject.GetComponent<LineRenderer>();
		_line.useWorldSpace = true;
		_line.startColor = _player.GetColor();
		_line.endColor = _player.GetColor();
		_isInitialized = true;
	}
	
	private void FixedUpdate () 
	{
		if(!_isInitialized)
			return;
		CreatePoints ();
	}

	public void UpdateParams(Vector2 pos, float angle)
	{
		_position = new Vector2(pos.x, pos.y);
		_angle = angle * Mathf.Deg2Rad;
	}

	public void SetAngle(float angle)
	{
		_angle = angle * Mathf.Deg2Rad;
	}
	
	private void CreatePoints ()
	{
		float x;
		float y;

		float angle = Utils.Vector2Extension.GetRadiant(_position);
				
		for (int i = 0; i < _segments; i++)
		{
			x = Mathf.Cos(angle) * _radius.x;
			y = Mathf.Sin(angle) * _radius.y * -1;

			_line.SetPosition (i,new Vector3(x,y,0));

			angle += (_angle / _segments);
		}
	}
}
