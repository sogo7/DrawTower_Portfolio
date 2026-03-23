using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace DrawTower.Ui
{
	public interface IScaleAnimToggle : IBaseUi
	{
		UniTask ShowAndAnimAsync(CancellationToken cancellationToken);
		void SetIsOn(bool isOn);
		void Clear();		
		Observable<bool> OnValueChangedAsObservable();
	}
}
