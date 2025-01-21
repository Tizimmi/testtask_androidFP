using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Global
{
	public class ColliderCollection : MonoBehaviour
	{
		public IReadOnlyList<Collider> Colliders => _colliders;
		[SerializeField]
		private List<Collider> _colliders;

		public void SetEnableColliders(bool value)
		{
			foreach (var item in _colliders)
				item.enabled = value;
		}
	}
}