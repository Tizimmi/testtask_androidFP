using ModestTree;
using System;
using System.Collections.Generic;

namespace Zenject
{
	[NoReflectionBaking]
	public class ResolveProvider : IProvider
	{
		public bool IsCached => false;

		public bool TypeVariesBasedOnMemberType => false;
		readonly DiContainer _container;
		readonly Type _contractType;
		readonly object _identifier;
		readonly bool _isOptional;
		readonly bool _matchAll;
		readonly InjectSources _source;

		public ResolveProvider(
			Type contractType,
			DiContainer container,
			object identifier,
			bool isOptional,
			InjectSources source,
			bool matchAll)
		{
			_contractType = contractType;
			_identifier = identifier;
			_container = container;
			_isOptional = isOptional;
			_source = source;
			_matchAll = matchAll;
		}

		public Type GetInstanceType(InjectContext context)
		{
			return _contractType;
		}

		public void GetAllInstancesWithInjectSplit(
			InjectContext context,
			List<TypeValuePair> args,
			out Action injectAction,
			List<object> buffer)
		{
			Assert.IsEmpty(args);
			Assert.IsNotNull(context);

			Assert.That(_contractType.DerivesFromOrEqual(context.MemberType));

			injectAction = null;
			if (_matchAll)
				_container.ResolveAll(GetSubContext(context), buffer);
			else
				buffer.Add(_container.Resolve(GetSubContext(context)));
		}

		InjectContext GetSubContext(InjectContext parent)
		{
			var subContext = parent.CreateSubContext(_contractType, _identifier);

			subContext.SourceType = _source;
			subContext.Optional = _isOptional;

			return subContext;
		}
	}
}