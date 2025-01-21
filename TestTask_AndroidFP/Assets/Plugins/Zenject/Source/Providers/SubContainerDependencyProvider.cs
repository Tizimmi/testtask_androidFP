using ModestTree;
using System;
using System.Collections.Generic;

namespace Zenject
{
	[NoReflectionBaking]
	public class SubContainerDependencyProvider : IProvider
	{
		public bool IsCached => false;

		public bool TypeVariesBasedOnMemberType => false;
		readonly Type _dependencyType;
		readonly object _identifier;
		readonly bool _resolveAll;
		readonly ISubContainerCreator _subContainerCreator;

		// if concreteType is null we use the contract type from inject context
		public SubContainerDependencyProvider(
			Type dependencyType,
			object identifier,
			ISubContainerCreator subContainerCreator,
			bool resolveAll)
		{
			_subContainerCreator = subContainerCreator;
			_dependencyType = dependencyType;
			_identifier = identifier;
			_resolveAll = resolveAll;
		}

		public Type GetInstanceType(InjectContext context)
		{
			return _dependencyType;
		}

		public void GetAllInstancesWithInjectSplit(
			InjectContext context,
			List<TypeValuePair> args,
			out Action injectAction,
			List<object> buffer)
		{
			Assert.IsNotNull(context);

			var subContainer = _subContainerCreator.CreateSubContainer(args, context, out injectAction);

			var subContext = CreateSubContext(context, subContainer);

			if (_resolveAll)
			{
				subContainer.ResolveAll(subContext, buffer);
				return;
			}

			buffer.Add(subContainer.Resolve(subContext));
		}

		InjectContext CreateSubContext(InjectContext parent, DiContainer subContainer)
		{
			var subContext = parent.CreateSubContext(_dependencyType, _identifier);

			subContext.Container = subContainer;

			// This is important to avoid infinite loops
			subContext.SourceType = InjectSources.Local;

			return subContext;
		}
	}
}