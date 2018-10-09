using UnityEngine;

public class Player : MonoBehaviour
{
	public bool Npc;
	private Transform _post;
	private float _score;
	private static int _currentPlayerCount;
	private Color32 _color;
	private Transform _transform;
	private SpriteRenderer _renderer;
	private float _speed = 2f;

	private BallManager _ballManager;
	private Ball _closestBall;

	private float _minAngle;
	private float _currentAngle;

	private bool _isInitialized;
	private bool _isFirstFrame;

	private void Awake()
	{
		_isInitialized = false;
		_isFirstFrame = true;
		Npc = true;
	}
	
	public float GetScore()
	{
		return _score;
	}

	public Color32 GetColor()
	{
		return _color;
	}
	
	public Transform GetPost()
	{
		return _post;
	}

	public void SetPost(Transform post)
	{
		_post = post;
	}
	
	public void Init(BallManager ballManager, float score, int playerCount, Color32 color)
	{
		_ballManager = ballManager;
		_score = score;
		_currentPlayerCount = playerCount;
		_color = color;
		_transform = gameObject.GetComponent<Transform>();
		_renderer = gameObject.GetComponent<SpriteRenderer>();
		_renderer.color = color;
		_currentAngle = 0;
		
		_isInitialized = true;
	}

	public static void SetPlayerCount(int playerCount)
	{
		_currentPlayerCount = playerCount;
	}
	
	private void FixedUpdate ()
	{
		if (!_isInitialized)
		{
			return;
		}

		_minAngle = Utils.Vector2Extension.GetRadiant(_post.position);

		if (_isFirstFrame)
		{
			_currentAngle = (_minAngle + (_score / 2));
			_isFirstFrame = false;
		}

		HandleInput();

		AdjustPosition();

		_score = (Mathf.PI * 2) / _currentPlayerCount;
	}

	private void HandleInput()
	{
		if (!Npc)
		{
			_currentAngle -= Input.GetAxis("Horizontal") * Time.fixedDeltaTime * _speed;
		}
		else
		{
			_closestBall = _ballManager.GetClosestBall(_transform);
			if (Utils.Vector2Extension.GetRadiant(_transform.position) >
			    Utils.Vector2Extension.GetRadiant(_closestBall.transform.position))
			{
				_currentAngle -= Time.fixedDeltaTime * _speed;
			}
			else
			{
				_currentAngle += Time.fixedDeltaTime * _speed;
			}
		}
	}

	private void AdjustPosition()
	{
		if (_currentAngle < _minAngle)
		{
			_currentAngle = _minAngle;
		}
		else if (_currentAngle > _minAngle + _score)
		{
			_currentAngle = _minAngle + _score;
		}
		_transform.eulerAngles  = new Vector3(0, 0, Mathf.Rad2Deg*-1*_currentAngle);
		_transform.position = new Vector3(Mathf.Cos(-1*_currentAngle), 
			                      Mathf.Sin(-1*_currentAngle), 0) * 14;
	}
}
