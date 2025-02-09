namespace Zenject
{
	// We extract the interface so that monobehaviours can be installers
	public interface IInstaller
	{
		bool IsEnabled { get; }
		void InstallBindings();
	}
}