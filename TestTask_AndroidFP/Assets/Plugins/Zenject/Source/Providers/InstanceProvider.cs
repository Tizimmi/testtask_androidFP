using ModestTree;
using System;
using System.Collections.Generic;

namespace Zenject
{
	[NoReflectionBaking]
	public class InstanceProvider : IProvider
	{
		public bool IsCached => true;

		public bool TypeVariesBasedOnMemberType => false;
		readonly DiContainer _container;
		readonly object _instance;
		readonly Type _instanceType;
		readonly Action<InjectContext, object> _instantiateCallback;

		public InstanceProvider(
			Type instanceType,
			object instance,
			DiContainer container,
			Action<InjectContext, object> instantiateCallback)
		{
			_instanceType = instanceType;
			_instance = instance;
			_container = container;
			_instantiateCallback = instantiateCallback;
		}

		public Type GetInstanceType(InjectContext context)
		{
			return _instanceType;
		}

		public void GetAllInstancesWithInjectSplit(
			InjectContext context,
			List<TypeValuePair> args,
			out Action injectAction,
			List<object> buffer)
		{
			Assert.That(args.Count == 0);
			Assert.IsNotNull(context);

			Assert.That(_instanceType.DerivesFromOrEqual(context.MemberType));

			injectAction = () =>
			{
				var instance = _container.LazyInject(_instance);

				if (_instantiateCallback != null)
					_instantiateCallback(context, instance);
			};

			buffer.Add(_instance);
		}
	}
}