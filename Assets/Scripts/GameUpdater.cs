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
		if (Input.GetKeyUp(KeyCode.Space))
		{
			_postAllignment.Kill();
			Destroy(_playerManager.GetPlayers()[0].gameObject);
			_playerManager.GetPlayers().RemoveAt(0);
			Player.SetPlayerCount(_playerManager.GetPlayers().Count);
		}
	}
}