using UnityEngine;

public class GameUpdater : MonoBehaviour
{
	private GameSettings _gameSettings;
	[SerializeField] private PostAllignment _postAllignment;
	[SerializeField] private PlayerManager _playerManager;
	
	private void Start()
	{
		_gameSettings = new GameSettings();

		_playerManager.Init(_gameSettings.InitialPlayerCount, _gameSettings.ColorList);
		_postAllignment.Init(_gameSettings.InitialPlayerCount, _gameSettings.CircleRange, _playerManager);
	}

	private void FixedUpdate()
	{
	}
}