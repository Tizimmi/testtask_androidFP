﻿#if !NOT_UNITY3D

using ModestTree;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject.Internal;

namespace Zenject
{
	[NoReflectionBaking]
	public class ScriptableObjectInstanceProvider : IProvider
	{
		public bool IsCached => false;

		public bool TypeVariesBasedOnMemberType => false;
		readonly object _concreteIdentifier;
		readonly DiContainer _container;
		readonly bool _createNew;
		readonly List<TypeValuePair> _extraArguments;
		readonly Action<InjectContext, object> _instantiateCallback;
		readonly UnityEngine.Object _resource;
		readonly Type _resourceType;

		public ScriptableObjectInstanceProvider(
			UnityEngine.Object resource,
			Type resourceType,
			DiContainer container,
			IEnumerable<TypeValuePair> extraArguments,
			bool createNew,
			object concreteIdentifier,
			Action<InjectContext, object> instantiateCallback)
		{
			_container = container;
			Assert.DerivesFromOrEqual<ScriptableObject>(resourceType);

			_resource = resource;
			_extraArguments = extraArguments.ToList();
			_resourceType = resourceType;
			_createNew = createNew;
			_concreteIdentifier = concreteIdentifier;
			_instantiateCallback = instantiateCallback;
		}

		public Type GetInstanceType(InjectContext context)
		{
			return _resourceType;
		}

		public void GetAllInstancesWithInjectSplit(
			InjectContext context,
			List<TypeValuePair> args,
			out Action injectAction,
			List<object> buffer)
		{
			Assert.IsNotNull(context);

			if (_createNew)
				buffer.Add(UnityEngine.ScriptableObject.Instantiate(_resource));
			else
				buffer.Add(_resource);

			injectAction = () =>
			{
				for (var i = 0; i < buffer.Count; i++)
				{
					var obj = buffer[i];

					var extraArgs = ZenPools.SpawnList<TypeValuePair>();

					extraArgs.AllocFreeAddRange(_extraArguments);
					extraArgs.AllocFreeAddRange(args);

					_container.InjectExplicit(obj,
						_resourceType,
						extraArgs,
						context,
						_concreteIdentifier);

					ZenPools.DespawnList(extraArgs);

					if (_instantiateCallback != null)
						_instantiateCallback(context, obj);
				}
			};
		}
	}
}

#endif