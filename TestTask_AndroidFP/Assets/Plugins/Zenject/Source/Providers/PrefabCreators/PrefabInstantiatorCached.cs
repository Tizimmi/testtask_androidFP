#if !NOT_UNITY3D

using ModestTree;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
	[NoReflectionBaking]
	public class PrefabInstantiatorCached : IPrefabInstantiator
	{
		public List<TypeValuePair> ExtraArguments => _subInstantiator.ExtraArguments;

		public Type ArgumentTarget => _subInstantiator.ArgumentTarget;

		public GameObjectCreationParameters GameObjectCreationParameters => _subInstantiator.GameObjectCreationParameters;

		GameObject _gameObject;
		readonly IPrefabInstantiator _subInstantiator;

		public PrefabInstantiatorCached(IPrefabInstantiator subInstantiator)
		{
			_subInstantiator = subInstantiator;
		}

		public UnityEngine.Object GetPrefab(InjectContext context)
		{
			return _subInstantiator.GetPrefab(context);
		}

		public GameObject Instantiate(InjectContext context, List<TypeValuePair> args, out Action injectAction)
		{
			// We can't really support arguments if we are using the cached value since
			// the arguments might change when called after the first time
			Assert.IsEmpty(args);

			if (_gameObject != null)
			{
				injectAction = null;
				return _gameObject;
			}

			_gameObject = _subInstantiator.Instantiate(context, new List<TypeValuePair>(), out injectAction);
			return _gameObject;
		}
	}
}

#endif