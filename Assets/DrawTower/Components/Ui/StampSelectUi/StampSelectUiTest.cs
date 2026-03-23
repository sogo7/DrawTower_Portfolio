using Cysharp.Threading.Tasks;
using DrawTower.Test;
using Zenject;
using R3;
using UnityEngine;

namespace DrawTower.Ui
{
	public class StampSelectUiTest : TestSceneBase
	{
		[Inject]
		private IStampSelectUi _stampSelectUi;
		
		private void Start()
		{	
			BindLog(_stampSelectUi.OnClickOpenBtnAsObservable(), "OnClickOpenBtnAsObservable");
			
			_stampSelectUi.OnClickSelectBtnAsObservable()
				.Subscribe(type => Debug.Log($"OnClickSelectBtnAsObservable: {type}"))
				.AddTo(this);
				
			BindButton(0, _stampSelectUi.ShowOpenBtnAsync);
			BindButton(1, _stampSelectUi.ShowSelectAreaAsync);
			BindButton(2, _stampSelectUi.HideSelectAreaAsync);
		}
	}
}
