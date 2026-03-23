namespace DrawTower.Logic
{
	public interface IAudioPlayer
	{
		bool IsBgmPlaying();
		bool IsSePlaying();
		bool IsMyCvPlaying();
		bool IsOpponentCvPlaying();
		void PlayBgm(BgmType bgmType);
		void SetBgmVolume(float volume);
		void StopBgm();
		void PlaySe(SeType seType, bool isLoop = false);
		void SetSeVolume(float volume);
		void StopSe();
		void PlayMyCv(CvType cvType);
		void StopMyCv();
		void PlayOpponentCv(CvType cvType);
		void StopOpponentCv();
	}
}

