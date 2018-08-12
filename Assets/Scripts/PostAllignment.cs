using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PostAllignment : MonoBehaviour
{
	private bool _isInitialized;
	private int _currentPlayerCount;
	private float _circleRange;
	private List<Transform> _postTransforms;
	private List<Post> _posts;
	
	private PlayerManager _playerManager;

	[SerializeField] private Transform _postRoot;
	[SerializeField] private GameObject _postPrefab;
	
	public void Init(int playerCount, float circleRange, PlayerManager playerManager)
	{
		_currentPlayerCount = playerCount;
		_circleRange = circleRange;
		_playerManager = playerManager;
		
		InitializePosts();
		
		_isInitialized = true;
	}
	
	private void Start ()
	{
		_isInitialized = false;
	}
	
	private void Update ()
	{
		if (!_isInitialized)
			return;
		
		AllignPosts();
	}

	private void InitializePosts()
	{
		_posts = new List<Post>();
		_postTransforms = new List<Transform>();
		
		for (int i = 0; i < _currentPlayerCount; i++)
		{
			_postTransforms.Add(Instantiate(_postPrefab, _postRoot).transform);
			_playerManager.GetPlayer(i).SetPost(_postTransforms[i]);
			_posts.Add(_postTransforms[i].GetComponent<Post>());
		}
		
		var direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

		for (int i = 0; i < _currentPlayerCount; i++)
		{
			_postTransforms[i].position = direction * _circleRange;
			direction = Utils.Vector2Extension.Rotate(direction, 
				_playerManager.GetPlayer(i).GetScore());
			
			_posts[i].Init(_playerManager.GetPlayer(i));
		}
	}
	
	private void AllignPosts()
	{
		var direction = _postTransforms[0].position.normalized;

		for (int i = 0; i < _currentPlayerCount; i++)
		{
			_postTransforms[i].position = direction * _circleRange;
			direction = Utils.Vector2Extension.Rotate(direction, _playerManager.GetPlayer(i).GetScore());
			_posts[i].UpdateParams(_postTransforms[i].position, 
				_playerManager.GetPlayer(i).GetScore());
		}
	}

	public void Kill()
	{
		if (_currentPlayerCount <= 0)
			return;
		Destroy(_postTransforms[0].gameObject);
		_postTransforms.RemoveAt(0);
		_posts.RemoveAt(0);
		_currentPlayerCount--;
		//Test
	}
}
