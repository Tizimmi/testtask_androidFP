using Game.Scripts.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Global
{
	public class GameSceneInstaller : MonoInstaller
	{
		[SerializeField]
		private Player _player;
		[SerializeField]
		private LevelItemRootProvider _levelItemRootProvider;
		[SerializeField]
		private GameEndTrigger _gameEndTrigger;
		[SerializeField]
		private GameUI _gameUi;

		public override void InstallBindings()
		{
			Container.BindInstance(_gameUi).AsSingle();
			Container.Bind<GameLoopManager>().AsSingle();
			Container.Bind<GameEndTrigger>().FromInstance(_gameEndTrigger).AsSingle();
			Container.Bind<Player>().FromInstance(_player).AsSingle();
			Container.Bind<HandModule>().FromResolveGetter<Player>(x => x.GetComponent<HandModule>()).AsSingle();
			Container.Bind<LevelItemRootProvider>().FromInstance(_levelItemRootProvider).AsSingle();
		}
	}
}