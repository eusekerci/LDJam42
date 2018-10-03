using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

	[SerializeField] private Transform _playerRoot;
	[SerializeField] private GameObject _playerPrefab;

	private List<Transform> _playerTransforms;
	private List<Player> _players;

	private BallManager _ballManager;
	private PostAllignment _postAllignment;
	private int _initialPlayerCount;

	public void Init(BallManager ballManager, int playerCount, List<Color> colorlist)
	{
		_ballManager = ballManager;
		
		_players = new List<Player>();
		_playerTransforms = new List<Transform>();
		
		for (int i = 0; i < playerCount; i++)
		{
			_playerTransforms.Add(Instantiate(_playerPrefab, _playerRoot).transform);
			_playerTransforms[i].name = "Player" + (i + 1).ToString();
			_players.Add(_playerTransforms[i].GetComponent<Player>());
		}
		
		for (int i = 0; i < playerCount; i++)
		{
			_players[i].Init(360 / playerCount, playerCount, colorlist[i]);
		}
	}

	public void SetPostAllignment(PostAllignment postAllignment)
	{
		_postAllignment = postAllignment;
	}

	public Player GetPlayer(int i)
	{
		return _players[i];
	}

	public void KillPlayer(int i)
	{
		if(_players.Count == 1)
			SceneManager.LoadScene(0);
		_postAllignment.KillPost(_players[i].GetPost().gameObject.name);
		_playerTransforms[i].gameObject.SetActive(false);
		_playerTransforms.RemoveAt(i);
		_players.RemoveAt(i);
		Player.SetPlayerCount(_players.Count);
		_postAllignment.UpdatePlayerNextPosts();

	}
}
