using ModestTree;
using System.Collections.Generic;

namespace Zenject
{
	public class HashSetPool<T> : StaticMemoryPool<HashSet<T>>
	{
		static readonly HashSetPool<T> _instance = new HashSetPool<T>();

		public static HashSetPool<T> Instance => _instance;

		public HashSetPool()
		{
			OnSpawnMethod = OnSpawned;
			OnDespawnedMethod = OnDespawned;
		}

		static void OnSpawned(HashSet<T> items)
		{
			Assert.That(items.IsEmpty());
		}

		static void OnDespawned(HashSet<T> items)
		{
			items.Clear();
		}
	}
}