using TMPro;
using UnityEngine;

namespace Game.Scripts.Global
{
	public class GameUI : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _winText;

		public void SetText(string text)
		{
			_winText.text = text;
		}
	}
}