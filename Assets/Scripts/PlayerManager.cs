using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

	[SerializeField] private Transform _playerRoot;
	[SerializeField] private GameObject _playerPrefab;

	private List<Transform> _playerTransforms;
	private List<Player> _players;

	private PostAllignment _postAllignment;
	private int _initialPlayerCount;

	public void Init(int playerCount, List<Color> colorlist)
	{
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

	public List<Player> GetPlayers()
	{
		return _players;
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
		Player.SetPlayerCount(GetPlayers().Count);
		_postAllignment.UpdatePlayerNextPosts();

	}

	public void KillPlayer(string name)
	{
		for (int i = 0; i < _playerTransforms.Count; i++)
		{
			if (name == _playerTransforms[i].gameObject.name)
			{
				KillPlayer(i);
				return;
			}
		}
	}
}
