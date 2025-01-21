namespace Zenject
{
	[NoReflectionBaking]
	public class IfNotBoundBinder
	{
		// Do not use this
		public BindInfo BindInfo { get; private set; }

		public IfNotBoundBinder(BindInfo bindInfo)
		{
			BindInfo = bindInfo;
		}

		public void IfNotBound()
		{
			BindInfo.OnlyBindIfNotBound = true;
		}
	}
}