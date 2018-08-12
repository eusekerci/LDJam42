using System.Collections.Generic;
using UnityEngine;

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

	public List<Player> GetPlayers()
	{
		return _players;
	}
	
	
	public Player GetPlayer(int i)
	{
		return _players[i];
	}
}
