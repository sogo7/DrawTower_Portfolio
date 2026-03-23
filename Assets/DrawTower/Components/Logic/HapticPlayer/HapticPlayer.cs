using System.Runtime.InteropServices;
using UnityEngine;

namespace DrawTower.Logic
{
    public class HapticPlayer : IHapticPlayer
    {
        [DllImport("__Internal")]
        private static extern void PlayHaptic();
        
        private bool _isEnabled = true;

        public void Play()
        {
            if (!_isEnabled) return;

            if (Application.platform == RuntimePlatform.IPhonePlayer)
                PlayHaptic();
        }
        
        public void SetEnabled(bool enabled)
        {
            _isEnabled = enabled;
        }
        
        public bool IsEnabled() => _isEnabled;
    }
}
