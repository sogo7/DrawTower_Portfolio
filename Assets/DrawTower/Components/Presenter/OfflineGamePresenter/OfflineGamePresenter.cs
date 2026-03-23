using R3;
using DrawTower.Logic;
using DrawTower.Token;
using Zenject;
using DrawTower.Ui;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Threading;
using DrawTower.Object;
using DrawTower.Model;

namespace DrawTower.Presenter
{
	public class OfflineGamePresenter : AsyncToken, IInitializable
	{
		//Logic		
		[Inject]
		private ISceneLoader _sceneLoader;
		
		[Inject]
		private IDrawLine _drawLine;
		
		[Inject]
		private ILineBlockCreator _lineBlockCreator;
		
		[Inject]
		private IBlockController _blockController;
		
		[Inject]
		private IBlockMonitor _blockMonitor;
		
		[Inject]
		private IAudioPlayer _audioPlayer;
		
		[Inject]
		private IHapticPlayer _hapticPlayer;
		
		//UI
		[Inject]
		private IDrawArea _drawArea;
		
		[Inject]
		private ICountdown _countdown;
		
		[Inject]
		private IFinish _finish;
		
		[Inject]
		private ITransition _transition;
		
		[Inject]
		private IInstructionUi _instructionUi;
		
		[Inject]
		private IRotateBtn _rotateBtn;
		
		[Inject]
		private ITimerUi _timerUi;
		
		[Inject]
		private IDialog _dialog;
		
		[Inject]
		private IPauseBtn _pauseBtn;
		
		[Inject]
		private IBg _bg;
		
		[Inject] 
		private IScore _score;
		
		//Object
		[Inject]
		private IKillZone _killZone;
		
		private int _gameTime = 60;
		private int _sumScore = 0;
		private float _maxHeight = 0f;
		private Player _player = new Player();
		
		private CompositeDisposable _container = new CompositeDisposable();

        public async void Initialize()
		{
			var token = GetToken();

			SetupEvents(token);
			await PrepareUiAsync(token);
			await RunGameLoopAsync(token);
		}

		private void SetupEvents(CancellationToken uiToken)
		{				
			Observable.Merge(
					_killZone.OnBlockOutOfBounds().AsUnitObservable(),
					_timerUi.OnTimeUp()
				)
				.Take(1)
				.SubscribeAwait(async (_, token) => 
				{					
					await FinishAsync(token);
				})
				.AddTo(_container);
				
			_lineBlockCreator.OnBlockScoreCreated()
				.Subscribe(score => _sumScore += score)
				.AddTo(_container);
				
			Observable.EveryUpdate()
				.Select(_ => _drawLine.IsDrawing())
				.DistinctUntilChanged()
				.Subscribe(isDrawing => 
				{
					if (isDrawing)
						_audioPlayer.PlaySe(SeType.Draw, true);
					else
						_audioPlayer.StopSe();
				})
				.AddTo(_container);
				
			_blockController.OnBlockLanded()
				.Subscribe(_ => 
				{
					_hapticPlayer.Play();
					_audioPlayer.PlaySe(SeType.Drop);
				})
				.AddTo(_container);
				
			_rotateBtn.OnClickAsObservable()
				.Subscribe(_ => 
				{
					PlayButtonFeedback();
				    _blockController.RotateSnapAsync(uiToken).Forget();
				})
				.AddTo(_container);
				
			_pauseBtn.OnClickAsObservable()
				.SubscribeAwait(async (_, token) => 
				{
					PlayButtonFeedback();
					await _dialog.ShowAndAnimAsync(DialogType.Replay, token);
				})
				.AddTo(_container);
				
			_dialog.OnClickYesBtnAsObservable()
				.SubscribeAwait(async (_, token) => 
				{
					PlayButtonFeedback();
				    await _dialog.HideAndAnimAsync(token);
				    
				    switch (_dialog.GetDialogType())
					{							
						case DialogType.Replay:
							await _transition.FadeInAsync(token);
							await _sceneLoader.LoadOfflineGameSceneAsync(token);
							break;
					}
				})
				.AddTo(_container);
				
			_dialog.OnClickNoBtnAsObservable()
				.SubscribeAwait(async (_, token) => 
				{
					PlayButtonFeedback();
				    await _dialog.HideAndAnimAsync(token);
				})
				.AddTo(_container);
		}

		private async UniTask PrepareUiAsync(CancellationToken token)
		{
			await _transition.FadeOutAsync(token);
			_bg.BgAnimAsync(token).Forget();
			
			_audioPlayer.SetBgmVolume(_player.bgm == "on" ? 1 : 0);
			_audioPlayer.SetSeVolume(_player.se == "on" ? 1 : 0);
			_hapticPlayer.SetEnabled(_player.vibration == "on");
			
			_audioPlayer.PlayBgm(BgmType.Game);
			
			_drawArea.Show();
			await _drawArea.TopAnimAsync(token);
			_pauseBtn.ShowAndAnimAsync(token).Forget();

			_countdown.Show();
			_audioPlayer.PlaySe(SeType.Countdown);
			await _countdown.StartCountdownAsync(CountdownType.Three, token);
			_audioPlayer.PlaySe(SeType.Countdown);
			await _countdown.StartCountdownAsync(CountdownType.Two, token);
			_audioPlayer.PlaySe(SeType.Countdown);
			await _countdown.StartCountdownAsync(CountdownType.One, token);
			_audioPlayer.PlaySe(SeType.Start);
			await _countdown.StartCountdownAsync(CountdownType.Start, token);
			_countdown.Hide();
		}

		private async UniTask RunGameLoopAsync(CancellationToken token)
		{
			var endTime = Time.time + _gameTime;
			
			_timerUi.StartTimerAsync(_gameTime).Forget();
			_score.ShowAndAnimAsync(token).Forget();
			
			while (Time.time < endTime && !token.IsCancellationRequested)
			{
				await PlayRoundAsync(token);
			}
		}

        private async UniTask PlayRoundAsync(CancellationToken token)
		{
			_drawLine.Cancel();
			await _drawArea.CenterAnimAsync(token);
			_drawLine.Setup(_drawArea.GetArea(), _player.GetColor());
			_instructionUi.ShowAndAnimAsync(InstructionType.OfflineDrawShape, token).Forget();
			var points = await _drawLine.StartDrawingAsync();
			_hapticPlayer.Play();
			_instructionUi.HideAndAnimAsync(token).Forget();
			await _drawArea.TopAnimAsync(token);
			var block = _lineBlockCreator.CreateOfflineBlock(points, _blockMonitor.GetSpawnPosY(), _player.GetColor());
			_audioPlayer.PlaySe(SeType.Spawn);
			_instructionUi.ShowAndAnimAsync(InstructionType.OfflinePlaceShape, token).Forget();
			await _rotateBtn.ShowAndAnimAsync(token);
			await _blockController.StartControllerAsync(block, token);
			await _rotateBtn.HideAndAnimAsync(token);
			await _blockMonitor.TrackBlockAsync(block.transform, token);
			
			if(_blockMonitor.GetSpawnPosY() > _maxHeight)
			{
				_maxHeight = _blockMonitor.GetSpawnPosY();
				_score.UpdateTextAsync(_maxHeight, 1f, token).Forget();
			}
				
			_instructionUi.HideAndAnimAsync(token).Forget();
		}
		
		private async UniTask FinishAsync(CancellationToken token)
		{
			_audioPlayer.PlaySe(SeType.Finish);
			await _finish.ShowAndAnimAsync(token);

			await _transition.FadeInAsync(token);
			await _sceneLoader.LoadOfflineGameSceneAsync(token);
		}
		
		private void PlayButtonFeedback()
        {
            _hapticPlayer.Play();
			_audioPlayer.PlaySe(SeType.Button);
        }
        
        public override void Dispose()
		{
			_container.Dispose();
			base.Dispose();
		}
    }
}
