using DG.Tweening;
using Game.Scripts.InteractableItemLogic;
using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerLogic
{
	public class HandModule : MonoBehaviour
	{
		[SerializeField]
		private Transform _root;
		[SerializeField]
		private HandInteraction _handInteraction;
		[SerializeField]
		private LayerMask _interactiveLayer;
		
		[Inject]
		private LevelItemRootProvider _rootProvider;
		
		private CollectibleItem CurrentItem { get; set; }
		private bool HasItem => CurrentItem != null;
		private Tweener _tween;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
				CastRay();
		}

		public void SetItem(CollectibleItem item, Tweener tweener)
		{
			if (HasItem)
				Debug.LogError("Already has item");

			_handInteraction.SetActive(true);
			_handInteraction.OnClick += RemoveItem;

			_tween = tweener;
			CurrentItem = item;
			CurrentItem.SetActiveColliders(false);

			CurrentItem.transform.SetParent(_root);

			var rigidbody = CurrentItem.GetComponent<Rigidbody>();
			rigidbody.useGravity = false;

			var playerCollider = GetComponent<CharacterController>();
			var itemColliders = CurrentItem._physicsColliders;

			foreach (var itemCollider in itemColliders.Colliders)
				Physics.IgnoreCollision(playerCollider, itemCollider, true);

			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}

		public void RemoveItem()
		{
			if (!HasItem)
				throw new Exception("Can't remove item, because hand is empty");

			_tween?.Kill();

			CurrentItem.SetActiveColliders(true);
			CurrentItem.transform.SetParent(_rootProvider.Root);
			var rigidbody = CurrentItem.GetComponent<Rigidbody>();

			rigidbody.useGravity = true;
			rigidbody.constraints = RigidbodyConstraints.None;

			CurrentItem = null;

			_handInteraction.SetActive(false);
			_handInteraction.OnClick -= RemoveItem;
		}

		private void CastRay()
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (!Physics.Raycast(ray,
					out var hit,
					3,
					_interactiveLayer))
				return;

			if (hit.collider.CompareTag("interactionLayer"))
				hit.collider.GetComponent<HandInteraction>().ClickHandler();

			if (HasItem)
				return;

			if (hit.collider.CompareTag("Collectible"))
				hit.collider.GetComponent<CollectibleItem>().Action();
		}
	}
}