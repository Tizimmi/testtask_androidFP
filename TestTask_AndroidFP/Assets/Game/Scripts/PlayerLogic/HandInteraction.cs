using System;
using UnityEngine;

namespace Game.Scripts.PlayerLogic
{
	public class HandInteraction : MonoBehaviour
	{
		[SerializeField]
		private Collider _collider;
		
		public event Action OnClick;

		public void ClickHandler()
		{
			if (_collider.enabled)
				OnClick?.Invoke();
		}

		public void SetActive(bool value)
		{
			_collider.enabled = value;
		}
	}
}