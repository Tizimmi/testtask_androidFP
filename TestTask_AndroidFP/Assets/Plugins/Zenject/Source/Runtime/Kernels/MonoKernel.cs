#if !NOT_UNITY3D

#pragma warning disable 649

using ModestTree;
using UnityEngine;

namespace Zenject
{
	public abstract class MonoKernel : MonoBehaviour
	{
		protected bool IsDestroyed => _isDestroyed;

		[InjectLocal]
		DisposableManager _disposablesManager = null;

		bool _hasInitialized;

		[InjectLocal]
		InitializableManager _initializableManager = null;
		bool _isDestroyed;
		[InjectLocal]
		TickableManager _tickableManager = null;

		[InjectOptional]
		private IDecoratableMonoKernel decoratableMonoKernel;

		public virtual void Start()
		{
			if (decoratableMonoKernel?.ShouldInitializeOnStart() ?? true)
				Initialize();
		}

		public virtual void Update()
		{
			// Don't spam the log every frame if initialization fails and leaves it as null
			if (_tickableManager != null)
			{
				if (decoratableMonoKernel != null)
					decoratableMonoKernel.Update();
				else
					_tickableManager.Update();
			}
		}

		public virtual void FixedUpdate()
		{
			// Don't spam the log every frame if initialization fails and leaves it as null
			if (_tickableManager != null)
			{
				if (decoratableMonoKernel != null)
					decoratableMonoKernel.FixedUpdate();
				else
					_tickableManager.FixedUpdate();
			}
		}

		public virtual void LateUpdate()
		{
			// Don't spam the log every frame if initialization fails and leaves it as null
			if (_tickableManager != null)
			{
				if (decoratableMonoKernel != null)
					decoratableMonoKernel.LateUpdate();
				else
					_tickableManager.LateUpdate();
			}
		}

		public virtual void OnDestroy()
		{
			// _disposablesManager can be null if we get destroyed before the Start event
			if (_disposablesManager != null)
			{
				Assert.That(!_isDestroyed);
				_isDestroyed = true;

				if (decoratableMonoKernel != null)
				{
					decoratableMonoKernel.Dispose();
					decoratableMonoKernel.LateDispose();
				}
				else
				{
					_disposablesManager.Dispose();
					_disposablesManager.LateDispose();
				}
			}
		}

		public void Initialize()
		{
			// We don't put this in start in case Start is overridden
			if (!_hasInitialized)
			{
				_hasInitialized = true;

				if (decoratableMonoKernel != null)
					decoratableMonoKernel.Initialize();
				else
					_initializableManager.Initialize();
			}
		}
	}
}

#endif