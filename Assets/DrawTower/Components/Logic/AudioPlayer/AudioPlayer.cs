using UnityEngine;

namespace DrawTower.Logic
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {    
        [SerializeField]
        private BgmDataSO _bgmDataSO;

        [SerializeField]
        private SeDataSO _seDataSO;
        
        [SerializeField]
        private CvDataSO _cvDataSO;

        [SerializeField]
        private AudioSource _bgmAudioSource;

        [SerializeField]
        private AudioSource _seAudioSource;
        
        [SerializeField] 
        private AudioSource _myCvAudioSource;
        
        [SerializeField] 
        private AudioSource _opponentCvAudioSource;
        
        public bool IsBgmPlaying() => _bgmAudioSource.isPlaying;
        public bool IsSePlaying() => _seAudioSource.isPlaying;
        public bool IsMyCvPlaying() => _myCvAudioSource.isPlaying;
        public bool IsOpponentCvPlaying() => _opponentCvAudioSource.isPlaying;

        public void PlayBgm(BgmType bgmType)
        {
            var audioClip = _bgmDataSO.GetClipByType(bgmType);

            if (_bgmAudioSource == null || audioClip == null) return;

            _bgmAudioSource.loop = true;
            _bgmAudioSource.clip = audioClip;
            _bgmAudioSource.Play();
        }

        public void SetBgmVolume(float volume)
        {
            if (_bgmAudioSource != null)
                _bgmAudioSource.volume = volume;
        }

        public void StopBgm()
        {
            if (_bgmAudioSource != null)
            {
                _bgmAudioSource.Stop();
                _bgmAudioSource.clip = null;
            }
        }

        public void PlaySe(SeType seType, bool isLoop = false)
        {
            var audioClip = _seDataSO.GetClipByType(seType);

            if (_seAudioSource == null || audioClip == null) return;
            
            if(isLoop)
            {
                _seAudioSource.loop = true;
                _seAudioSource.clip = audioClip;
                _seAudioSource.Play();
            }       
            else
            {          
                _seAudioSource.loop = false;
                _seAudioSource.PlayOneShot(audioClip);  
            }
        }

        public void SetSeVolume(float volume)
        {
            if (_seAudioSource != null)
                _seAudioSource.volume = volume;
                
            SetMyCvVolume(volume);
            SetOpponentCvVolume(volume);
        }

        public void StopSe()
        {
            if (_seAudioSource != null)
            {
                _seAudioSource.Stop();
                _seAudioSource.clip = null;
            }
        }
        
        public void PlayMyCv(CvType cvType)
        {
            var audioClip = _cvDataSO.GetClipByType(cvType);

            if (_myCvAudioSource == null || audioClip == null) return;
            
            _myCvAudioSource.loop = false;
                _myCvAudioSource.PlayOneShot(audioClip);
        }

        private void SetMyCvVolume(float volume)
        {
            if (_myCvAudioSource != null)
                _myCvAudioSource.volume = volume;
        }

        public void StopMyCv()
        {
            if (_myCvAudioSource != null)
            {
                _myCvAudioSource.Stop();
                _myCvAudioSource.clip = null;
            }
        }
        
        public void PlayOpponentCv(CvType cvType)
        {
            var audioClip = _cvDataSO.GetClipByType(cvType);

            if (_opponentCvAudioSource == null || audioClip == null) return;
            
            _opponentCvAudioSource.loop = false;
                _opponentCvAudioSource.PlayOneShot(audioClip);
        }

        private void SetOpponentCvVolume(float volume)
        {
            if (_opponentCvAudioSource != null)
                _opponentCvAudioSource.volume = volume;
        }

        public void StopOpponentCv()
        {
            if (_opponentCvAudioSource != null)
            {
                _opponentCvAudioSource.Stop();
                _opponentCvAudioSource.clip = null;
            }
        }

        public void Dispose()
        {
            StopBgm();
            StopSe();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
