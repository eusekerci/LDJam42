using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Transform _post;
	private int _score;
	private static int _currentPlayerCount;
	private Color32 _color;

	public int GetScore()
	{
		return _score;
	}

	public Color32 GetColor()
	{
		return _color;
	}

	public void SetColor(Color32 color)
	{
		_color = color;
	}

	public void SetPost(Transform post)
	{
		_post = post;
	}
	
	public void Init(int score, int playerCount, Color32 color)
	{
		_score = score;
		_currentPlayerCount = playerCount;
		_color = color;
	}

	public static void SetPlayerCount(int playerCount)
	{
		_currentPlayerCount = playerCount;
	}
	
	private void FixedUpdate ()
	{
		_score = 360 / _currentPlayerCount;
	}
}
