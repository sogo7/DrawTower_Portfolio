using UnityEngine;
using UnityEngine.Events;
using DrawTower.Token;

namespace DrawTower.Ui
{
	public class BaseUi : AsyncTokenMonoBehaviour
	{		
		private void Start()
		{
			var rect = gameObject.GetComponent<RectTransform>();
			if(rect != null) ApplySafeArea(rect);	    
		}
		
		private void ApplySafeArea(RectTransform rect)
		{
			var safeArea = Screen.safeArea;

			var anchorMinX = safeArea.xMin / Screen.width;
			var anchorMinY = safeArea.yMin / Screen.height;
			var anchorMaxX = safeArea.xMax / Screen.width;
			var anchorMaxY = safeArea.yMax / Screen.height;

			rect.anchorMin = new Vector2(anchorMinX, anchorMinY);
			rect.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
		}
		
		public virtual void Show()
		{
			gameObject.SetActive(true);
		}
		
		public virtual void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
