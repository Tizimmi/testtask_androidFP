using ModestTree;
using System.Collections.Generic;

namespace Zenject
{
	public class DictionaryPool<TKey, TValue> : StaticMemoryPool<Dictionary<TKey, TValue>>
	{
		static readonly DictionaryPool<TKey, TValue> _instance = new DictionaryPool<TKey, TValue>();

		public static DictionaryPool<TKey, TValue> Instance => _instance;

		public DictionaryPool()
		{
			OnSpawnMethod = OnSpawned;
			OnDespawnedMethod = OnDespawned;
		}

		static void OnSpawned(Dictionary<TKey, TValue> items)
		{
			Assert.That(items.IsEmpty());
		}

		static void OnDespawned(Dictionary<TKey, TValue> items)
		{
			items.Clear();
		}
	}
}