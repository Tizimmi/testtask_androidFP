#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public abstract class SubContainerCreatorDynamicContext : ISubContainerCreator
	{
		protected DiContainer Container => _container;
		readonly DiContainer _container;

		public SubContainerCreatorDynamicContext(DiContainer container)
		{
			_container = container;
		}

		public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext parentContext, out Action injectAction)
		{
			bool shouldMakeActive;
			var gameObj = CreateGameObject(parentContext, out shouldMakeActive);

			var context = gameObj.AddComponent<GameObjectContext>();

			AddInstallers(args, context);

			context.Install(_container);

			injectAction = () =>
			{
				// Note: We don't need to call ResolveRoots here because GameObjectContext does this for us
				_container.Inject(context);

				if (shouldMakeActive && !_container.IsValidating)
				{
#if ZEN_INTERNAL_PROFILING
                    using (ProfileTimers.CreateTimedBlock("User Code"))
#endif
					{
						gameObj.SetActive(true);
					}
				}
			};

			return context.Container;
		}

		protected abstract void AddInstallers(List<TypeValuePair> args, GameObjectContext context);
		protected abstract GameObject CreateGameObject(InjectContext context, out bool shouldMakeActive);
	}
}

#endif