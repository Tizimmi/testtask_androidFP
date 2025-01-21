using UnityEngine;

namespace Game.Scripts.PlayerLogic
{
	public class Player : MonoBehaviour
	{
		[SerializeField]
		private PlayerController _playerController;
		[SerializeField]
		private HandModule _handModule;
	}
}