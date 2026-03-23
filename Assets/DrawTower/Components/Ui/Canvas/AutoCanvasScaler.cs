using UnityEngine;
using UnityEngine.UI;

namespace DrawTower.Ui
{
    public class AutoCanvasScaler : MonoBehaviour
    {
        private void Start()
        {
            var canvasScaler = gameObject.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        }
    }
}

