using System;
using DrawTower.Test;
using UnityEngine;
using Zenject;

namespace DrawTower.Logic
{
	public class AudioPlayerTest : TestSceneBase
	{
		[Inject]
		private IAudioPlayer _audioPlayer;
		private int _bgmIndex = 0;
		private int _seIndex = 0;
		private int _cvIndex = 0;
		
		private void Start()
		{
			BindButton(0, () => Debug.Log($"IsBgmPlaying: {_audioPlayer.IsBgmPlaying()}"));
			BindButton(1, () => Debug.Log($"IsSePlaying: {_audioPlayer.IsSePlaying()}"));
			BindButton(2, () => 
			{
				if (Enum.IsDefined(typeof(BgmType), _bgmIndex))
				{
					_audioPlayer.PlayBgm((BgmType)_bgmIndex);
					_bgmIndex++;
				}
				else
				{
					_bgmIndex = 0;
					_audioPlayer.PlayBgm((BgmType)_bgmIndex);
					_bgmIndex++;
				}
			});
			BindButton(3, () => _audioPlayer.SetBgmVolume(0f));
			BindButton(4, () => _audioPlayer.StopBgm());
			BindButton(5, () => 
			{
				if (Enum.IsDefined(typeof(SeType), _seIndex))
				{
					_audioPlayer.PlaySe((SeType)_seIndex, true);
					_seIndex++;
				}
				else
				{
					_seIndex = 0;
					_audioPlayer.PlaySe((SeType)_seIndex, true);
					_seIndex++;
				}
			});
			BindButton(6, () => _audioPlayer.SetSeVolume(0f));
			BindButton(7, () => _audioPlayer.StopSe());
			BindButton(8, () => 
			{
				if (Enum.IsDefined(typeof(CvType), _cvIndex))
				{
					_audioPlayer.PlayMyCv((CvType)_cvIndex);
					_cvIndex++;
				}
				else
				{
					_cvIndex = 0;
					_audioPlayer.PlayMyCv((CvType)_cvIndex);
					_cvIndex++;
				}
			});
			BindButton(9, () => _audioPlayer.StopMyCv());
			BindButton(10, () => 
			{
				if (Enum.IsDefined(typeof(CvType), _cvIndex))
				{
					_audioPlayer.PlayOpponentCv((CvType)_cvIndex);
					_cvIndex++;
				}
				else
				{
					_cvIndex = 0;
					_audioPlayer.PlayOpponentCv((CvType)_cvIndex);
					_cvIndex++;
				}
			});
			BindButton(11, () => _audioPlayer.StopOpponentCv());
		}
	}
}
