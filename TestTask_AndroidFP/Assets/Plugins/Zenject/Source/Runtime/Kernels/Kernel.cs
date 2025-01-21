using System;
using System.Diagnostics;

namespace Zenject
{
	[DebuggerStepThrough]
	public class Kernel : IInitializable, IDisposable, ITickable, ILateTickable, IFixedTickable, ILateDisposable
	{
		[InjectLocal]
		DisposableManager _disposablesManager = null;

		[InjectLocal]
		InitializableManager _initializableManager = null;
		[InjectLocal]
		TickableManager _tickableManager = null;

		public virtual void Dispose()
		{
			_disposablesManager.Dispose();
		}

		public virtual void FixedTick()
		{
			_tickableManager.FixedUpdate();
		}

		public virtual void Initialize()
		{
			_initializableManager.Initialize();
		}

		public virtual void LateDispose()
		{
			_disposablesManager.LateDispose();
		}

		public virtual void LateTick()
		{
			_tickableManager.LateUpdate();
		}

		public virtual void Tick()
		{
			_tickableManager.Update();
		}
	}
}