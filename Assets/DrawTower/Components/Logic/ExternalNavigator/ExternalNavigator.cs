using UnityEngine;

namespace DrawTower.Logic
{
	public class ExternalNavigator : IExternalNavigator
	{
		/// <summary>
		/// アプリからURLを開く
		/// </summary>
		public void Open(string url)
		{
			Application.OpenURL(url);
		}
	}
}

