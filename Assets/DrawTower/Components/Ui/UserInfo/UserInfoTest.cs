using Cysharp.Threading.Tasks;
using DrawTower.Model;
using DrawTower.Test;
using UnityEngine;
using Zenject;
using R3;

namespace DrawTower.Ui
{
	public class UserInfoTest : TestSceneBase
	{
		[Inject]
		private IUserInfo _userInfo;
		
		private void Start()
		{			
			var player = new Player()
			    {
			        colorId = "0"
			    };
			
			BindButton(0, _userInfo.Show);
			BindButton(1, _userInfo.Hide);
			BindButton(2, () => _userInfo.SetUserInfo("テスト君", player, new RankingData("7位くん", 10000, "7", "0")));
			BindButton(3, () => 
			{
			    Debug.Log($"name: {_userInfo.GetName()}");
			    Debug.Log($"color_id: {_userInfo.GetColorId()}");
			});
			BindButton(4, _userInfo.MoveCenterAnimAsync);
			BindButton(5, _userInfo.MoveRightAnimAsync);
			BindButton(6, _userInfo.ShowNgWordAlert);
			BindButton(7, () => _userInfo.ApplyName("test"));
			BindButton(8, () => _userInfo.SetEditMode(true));
			BindButton(9, () => _userInfo.SetEditMode(false));
			
			BindLog(_userInfo.OnClickEditBtnAsObservable(), "OnClickEditBtnAsObservable");
			
			_userInfo.OnEndEditAsObservable()
				.Subscribe(_ => Debug.Log("OnEndEditAsObservable"))
				.AddTo(this);
		}
	}
}
