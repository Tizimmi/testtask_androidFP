namespace Game.Scripts.Global
{
	public class GameLoopManager
	{
		private readonly GameUI _gameUI;

		public GameLoopManager(GameUI gameUI)
		{
			_gameUI = gameUI;
		}
		
		public void GameOver()
		{
			_gameUI.SetText("CONGRATULATIONS! YOU HAVE WON THE GAME!");
		}
	}
}