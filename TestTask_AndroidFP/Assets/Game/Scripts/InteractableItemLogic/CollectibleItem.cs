using DG.Tweening;
using Game.Scripts.Global;
using Game.Scripts.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.InteractableItemLogic
{
	[RequireComponent(typeof(Rigidbody))]
	public class CollectibleItem : MonoBehaviour, IInteractable
	{
		public string _name;
		[SerializeField]
		public ColliderCollection _interactiveColliders;
		[SerializeField]
		public ColliderCollection _physicsColliders;

		[Inject]
		private readonly HandModule _handModule;

		public void Action()
		{
			var tween = transform.DOLocalMove(Vector3.zero, 0.5f);
			_handModule.SetItem(this, tween);
		}

		public void SetActiveColliders(bool value)
		{
			foreach (var collider in _interactiveColliders?.Colliders)
				collider.enabled = value;

			foreach (var collider in _physicsColliders?.Colliders)
				collider.enabled = value;
		}
	}
}