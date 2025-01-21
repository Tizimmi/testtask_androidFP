namespace Zenject
{
	public abstract class InstallerBase : IInstaller
	{
		protected DiContainer Container => _container;

		public virtual bool IsEnabled => true;
		[Inject]
		DiContainer _container = null;

		public abstract void InstallBindings();
	}
}