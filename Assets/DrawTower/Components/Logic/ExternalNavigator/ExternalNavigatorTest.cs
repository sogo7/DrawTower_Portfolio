using UnityEngine;
using Zenject;
using DrawTower.Test;

namespace DrawTower.Logic
{
	public class ExternalNavigatorTest : TestSceneBase
	{
		[Inject]
		private IExternalNavigator _externalNavigator;
		
		[SerializeField]
		private string _appLink = "https://example.com/portfolio-app";

		private  void Start()
		{			
			BindButton(0, () => 
			{
			    _externalNavigator.Open(_appLink);
			});
		}
	}
}
