using System.Threading;
using Cysharp.Threading.Tasks;
using DrawTower.Model;
using R3;

namespace DrawTower.Ui
{
	public interface IUserInfo : IBaseUi
	{
		void SetUserInfo(string name, Player player, RankingData rankingData);
		string GetName();
		string GetAppliedName();
		string GetColorId();
		UniTask MoveCenterAnimAsync(CancellationToken ct);
		UniTask MoveRightAnimAsync(CancellationToken ct);
		bool IsNameLengthInvalid(string name);
		void ShowNgWordAlert();
		void ApplyName(string name);
		void SetEditMode(bool isEdit);
		Observable<string> OnEndEditAsObservable();
		Observable<Unit> OnClickEditBtnAsObservable();
	}
}
