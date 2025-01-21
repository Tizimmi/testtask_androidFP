using Game.Scripts.InteractableItemLogic;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Global
{
	[RequireComponent(typeof(Collider))]
	public class GameEndTrigger : MonoBehaviour
	{
		[SerializeField]
		private List<string> _requiredItems;
		[Inject]
		private readonly GameLoopManager _gameLoopManager;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Collectible"))
				return;

			var collectibleItem = other.GetComponent<CollectibleItem>();

			if (_requiredItems.Contains(collectibleItem._name))
			{
				_requiredItems.Remove(collectibleItem._name);
				Destroy(collectibleItem.gameObject);
			}

			if (_requiredItems.Count == 0)
				_gameLoopManager.GameOver();
		}
	}
}