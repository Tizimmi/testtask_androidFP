using ModestTree;
using System;
using System.Collections.Generic;
using Zenject.Internal;

namespace Zenject
{
	[NoReflectionBaking]
	public class BindStatement : IDisposable
	{
		public BindingInheritanceMethods BindingInheritanceMethod
		{
			get
			{
				AssertHasFinalizer();
				return _bindingFinalizer.BindingInheritanceMethod;
			}
		}

		public bool HasFinalizer => _bindingFinalizer != null;
		IBindingFinalizer _bindingFinalizer;
		readonly List<IDisposable> _disposables;

		public BindStatement()
		{
			_disposables = new List<IDisposable>();
			Reset();
		}

		public void Dispose()
		{
			ZenPools.DespawnStatement(this);
		}

		public void SetFinalizer(IBindingFinalizer bindingFinalizer)
		{
			_bindingFinalizer = bindingFinalizer;
		}

		void AssertHasFinalizer()
		{
			if (_bindingFinalizer == null)
				throw Assert.CreateException("Unfinished binding!  Some required information was left unspecified.");
		}

		public void AddDisposable(IDisposable disposable)
		{
			_disposables.Add(disposable);
		}

		public BindInfo SpawnBindInfo()
		{
			var bindInfo = ZenPools.SpawnBindInfo();
			AddDisposable(bindInfo);
			return bindInfo;
		}

		public void FinalizeBinding(DiContainer container)
		{
			AssertHasFinalizer();
			_bindingFinalizer.FinalizeBinding(container);
		}

		public void Reset()
		{
			_bindingFinalizer = null;

			for (var i = 0; i < _disposables.Count; i++)
				_disposables[i].Dispose();

			_disposables.Clear();
		}
	}
}