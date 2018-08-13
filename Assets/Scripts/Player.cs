using UnityEngine;

public class Player : MonoBehaviour
{
	public bool Npc;
	private Transform _post;
	private Transform _nextPost;
	private int _score;
	private static int _currentPlayerCount;
	private Color32 _color;
	private Transform _transform;
	private SpriteRenderer _renderer;
	private float _speed = 150f;

	private float _minAngle;
	private float _maxAngle;
	private float _currentAngle;

	private bool _isInitialized;

	private void Awake()
	{
		_isInitialized = false;
		Npc = false;
	}
	
	public int GetScore()
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

	public void SetNextPost(Transform post)
	{
		_nextPost = post;
	}
	
	public void Init(int score, int playerCount, Color32 color)
	{
		_score = score;
		_currentPlayerCount = playerCount;
		_color = color;
		_transform = gameObject.GetComponent<Transform>();
		_renderer = gameObject.GetComponent<SpriteRenderer>();
		_renderer.color = color;
		_currentAngle = -1234;
		
		_isInitialized = true;
	}

	public static void SetPlayerCount(int playerCount)
	{
		_currentPlayerCount = playerCount;
	}
	
	private void FixedUpdate ()
	{
		if (!_isInitialized)
			return;
		
		_minAngle = Utils.Vector2Extension.GetAngle(_post.position);
		_maxAngle = Utils.Vector2Extension.GetAngle(_nextPost.position);

		if (_currentAngle <= -1000)
		{
			_currentAngle = _maxAngle < _minAngle
				? ((_maxAngle + _minAngle + 360) / 2.0f) % 360
				: ((_maxAngle + _minAngle) / 2.0f) % 360;
		}

		HandleInput();

		AdjustPosition();

		_score = 360 / _currentPlayerCount;
	}

	private void HandleInput()
	{
		if (!Npc)
		{
			_currentAngle -= Input.GetAxis("Horizontal") * Time.fixedDeltaTime * _speed;
		}
		else
		{
			//TODO Implement Basic AI
		}
	}

	private void AdjustPosition()
	{
		//TODO Find a better splution
		_currentAngle = (_currentAngle + 360) % 360;
		if (_currentAngle > _maxAngle)
		{
			if (_maxAngle > _minAngle)
			{
				_currentAngle = _maxAngle;
			}
			else if (_currentAngle < 180)
			{
				_currentAngle = _maxAngle;
			}
		}

		if (_currentAngle < _minAngle)
		{
			if (_maxAngle > _minAngle)
			{
				_currentAngle = _minAngle;
			}
			else if (_currentAngle > 180)
			{
				_currentAngle = _minAngle;
			}
		}
		
		_transform.eulerAngles  = new Vector3(0, 0, -1*_currentAngle);
		_transform.position = new Vector3(Mathf.Cos(Mathf.Deg2Rad * -1*_currentAngle), 
			                      Mathf.Sin(Mathf.Deg2Rad * -1*_currentAngle), 0) * 14;
	}
}
