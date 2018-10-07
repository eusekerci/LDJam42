using UnityEngine;

public class GameUpdater : MonoBehaviour
{
	private GameSettings _gameSettings;
	[SerializeField] private PostAllignment _postAllignment;
	[SerializeField] private PlayerManager _playerManager;
	[SerializeField] private BallManager _ballManager;
	
	private void Start()
	{
		_gameSettings = new GameSettings();

		_ballManager.Init(_gameSettings.BallCount, _playerManager, _postAllignment);
		_playerManager.Init(_ballManager, _gameSettings.InitialPlayerCount, _gameSettings.ColorList);
		_postAllignment.Init(_playerManager, _gameSettings.InitialPlayerCount, _gameSettings.CircleRange);
	}
}