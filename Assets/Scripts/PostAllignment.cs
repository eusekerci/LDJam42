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
	
	public void Init(PlayerManager playerManager, int playerCount, float circleRange)
	{
		_currentPlayerCount = playerCount;
		_circleRange = circleRange;
		_playerManager = playerManager;
		
		InitializePosts();
		
		playerManager.SetPostAllignment(this);
		
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
			_postTransforms[i].name = "Post" + (i + 1).ToString();
			_playerManager.GetPlayer(i).SetPost(_postTransforms[i]);
			_posts.Add(_postTransforms[i].GetComponent<Post>());
		}

		UpdatePlayerNextPosts();
		
		var direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

		for (int i = 0; i < _currentPlayerCount; i++)
		{
			_postTransforms[i].position = direction * _circleRange;
			direction = Utils.Vector2Extension.Rotate(direction, 
				_playerManager.GetPlayer(i).GetScore());
			
			_posts[i].Init(_playerManager.GetPlayer(i));
		}
	}

	public List<Transform> GetPostTransforms()
	{
		return _postTransforms;
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

	public void UpdatePlayerNextPosts()
	{
		for (int i = 0; i < _currentPlayerCount; i++)
		{
			int a = i - 1 < 0 ? _currentPlayerCount - 1 : i-1;
			_playerManager.GetPlayer(i).SetNextPost(_postTransforms[a]);
		}
	}

	private void KillPost(int i)
	{
		_postTransforms[i].gameObject.SetActive(false);
		_postTransforms.RemoveAt(i);
		_posts.RemoveAt(i);
		_currentPlayerCount--;

		for (int a = 0; a < _posts.Count; a++)
		{
			_posts[a].SetAngle(360/_currentPlayerCount);
		}
	}

	public void KillPost(string n)
	{
		for (int i = 0; i < _postTransforms.Count; i++)
		{
			if (n == _postTransforms[i].gameObject.name)
			{
				KillPost(i);
				return;
			}
		}
	}
}
