#if !NOT_UNITY3D

using ModestTree;
using System;
using System.Collections.Generic;

namespace Zenject
{
	[NoReflectionBaking]
	public class InstantiateOnPrefabComponentProvider : IProvider
	{
		public bool IsCached => false;

		public bool TypeVariesBasedOnMemberType => false;
		readonly Type _componentType;
		readonly IPrefabInstantiator _prefabInstantiator;

		// if concreteType is null we use the contract type from inject context
		public InstantiateOnPrefabComponentProvider(Type componentType, IPrefabInstantiator prefabInstantiator)
		{
			_prefabInstantiator = prefabInstantiator;
			_componentType = componentType;
		}

		public Type GetInstanceType(InjectContext context)
		{
			return _componentType;
		}

		public void GetAllInstancesWithInjectSplit(
			InjectContext context,
			List<TypeValuePair> args,
			out Action injectAction,
			List<object> buffer)
		{
			Assert.IsNotNull(context);

			var gameObject = _prefabInstantiator.Instantiate(context, args, out injectAction);

			var component = gameObject.AddComponent(_componentType);

			buffer.Add(component);
		}
	}
}

#endif