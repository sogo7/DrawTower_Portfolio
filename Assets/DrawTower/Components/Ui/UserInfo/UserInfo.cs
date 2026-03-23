using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using DrawTower.Model;
using R3;

namespace DrawTower.Ui
{
	public class UserInfo : BaseUi, IUserInfo
	{
		[SerializeField]
		private RectTransform _rect;
		
		[SerializeField]
		private TextMeshProUGUI _alert;
		
		[SerializeField] 
		private TMP_InputField _nameInputField;
		
		[SerializeField] 
		private ScaleAnimBtn _editBtn;
		
		[SerializeField] 
		private Toggle[] _colorToggles;
		
		[SerializeField] 
		private TextMeshProUGUI _rank;
		
		[SerializeField] 
		private TextMeshProUGUI _score;
		
		private const float RIGHT_POS_X = 1500f;
		private float _duration = 0.5f;
		private Tween _tween;	
		private int _colorId;
		private string _defaultName;
		private bool _isMoving = false;
		
		public void SetUserInfo(string name, Player player, RankingData rankingData)
		{
		    SetupNameInputField(name);
		    SetupColorToggles();
		    
		    if(rankingData.score != 0)
		    {
		        _rank.text = $"{rankingData.rank}位";
		        float lastHeightMeters = rankingData.score / 100f;
		    	_score.text = $"{lastHeightMeters:F1}m";
		    }

		    _colorId = int.Parse(player.colorId);
		    for (int i = 0; i < _colorToggles.Length; i++)
				_colorToggles[i].SetIsOnWithoutNotify(i == _colorId);
		}
		
		public string GetName() => _nameInputField.text;
		public string GetAppliedName() => _defaultName;
		public string GetColorId() => _colorId.ToString();
		
		public async UniTask MoveCenterAnimAsync(CancellationToken ct)
		{
			if (_isMoving) return;
            _isMoving = true;
            
			_alert.gameObject.SetActive(false);
			Show();
		    _tween = _rect.DOAnchorPosX(0f, _duration)
		    	.SetEase(Ease.OutCubic);
			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
			
			_isMoving = false;
		}
		
		public async UniTask MoveRightAnimAsync(CancellationToken ct)
		{
			if (_isMoving) return;
            _isMoving = true;
            
		    _tween = _rect.DOAnchorPosX(RIGHT_POS_X, _duration)
		    	.SetEase(Ease.OutCubic);
			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
			Hide();
			
			_isMoving = false;
		}
		
		private void SetupNameInputField(string defaultName)
		{
			_defaultName = defaultName;
			_nameInputField.text = defaultName;
			SetEditMode(false);
		}
		
		public bool IsNameLengthInvalid(string name)
		{
			var isInvalid = name.Length < 3 || name.Length > 7;
		    if (isInvalid)
				SetAlert("名前は3〜7文字で入力してください");
			
			return isInvalid;
		}
		
		public void ShowNgWordAlert()
		{
			SetAlert("不適切な名前です");
		}
		
		public void ApplyName(string name)
		{
		    _defaultName = name;
			_alert.gameObject.SetActive(false);
		}
		
		public void SetEditMode(bool isEdit)
		{
			_nameInputField.interactable = isEdit;
			_nameInputField.readOnly = !isEdit;

			if (isEdit)
			{
				_nameInputField.ActivateInputField();
				_nameInputField.Select();
			}
			else
			{
				_nameInputField.DeactivateInputField();
			}
		}

		
		private void SetAlert(string alert)
		{
		    _alert.gameObject.SetActive(true);
			_alert.text = alert;
			_alert.color = Color.red;
			_nameInputField.text = _defaultName;
		}
		
		private void SetupColorToggles()
		{
			for (int i = 0; i < _colorToggles.Length; i++)
			{
				int index = i;
				_colorToggles[i].onValueChanged.AddListener(isOn =>
				{
					if (!isOn)
					{
						if (_colorId == index)
							_colorToggles[index].SetIsOnWithoutNotify(true);
						return;
					}
					if (isOn)
					{
						_colorId = index;
						for (int i = 0; i < _colorToggles.Length; i++)
						{
							if (i != index)
								_colorToggles[i].SetIsOnWithoutNotify(false);
						}
					}
				});
			}
		}
		
		public Observable<string> OnEndEditAsObservable() => _nameInputField.onEndEdit.AsObservable();
		
		public Observable<Unit> OnClickEditBtnAsObservable() => _editBtn.OnClickAsObservable();
		
		protected override void OnDestroy()
		{
			_tween?.Kill();
			base.OnDestroy();
		}
	}
}
