using UnityEngine;

namespace Game.Scripts.PlayerLogic
{
	public class LevelItemRootProvider : MonoBehaviour
	{
		[field: SerializeField]
		public Transform Root { get; private set; }
	}
}