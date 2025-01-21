using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
	public class GarageDoor : MonoBehaviour
	{
		[SerializeField]
		private Transform _openedPosition;

		private void Start()
		{
			var tween = transform.DOMove(_openedPosition.position, 2f);
		}
	}
}