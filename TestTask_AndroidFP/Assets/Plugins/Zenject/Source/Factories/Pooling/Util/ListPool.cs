using System.Collections.Generic;

namespace Zenject
{
	public class ListPool<T> : StaticMemoryPool<List<T>>
	{
		static readonly ListPool<T> _instance = new ListPool<T>();

		public static ListPool<T> Instance => _instance;

		public ListPool()
		{
			OnDespawnedMethod = OnDespawned;
		}

		void OnDespawned(List<T> list)
		{
			list.Clear();
		}
	}
}