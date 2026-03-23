using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DrawTower.Ui
{
	public class StampUi : BaseUi, IStampUi
	{
		[SerializeField]
		private StampAnim[] _stampAnimPrefabs;
		
		[SerializeField]
        private RectTransform _myRoot;

        [SerializeField]
        private RectTransform _opponentRoot;
		
		private Dictionary<(StampType, StampSide), StampAnim> _cache 
			= new Dictionary<(StampType, StampSide), StampAnim>();
		
		private bool _isMyMoving;
		
		private bool _isOpponentMoving;

		public async UniTask ShowAndAnimAsync(StampType type, StampSide side, CancellationToken ct)
		{
			if (IsMoving(side))
        		return;
			
			SetMoving(side, true);

			try
			{
				await GetOrCreate(type, side).ShowAndAnimAsync(ct);
			}
			finally
			{
				SetMoving(side, false);
			}
		}
		
		private StampAnim GetOrCreate(StampType type, StampSide side)
        {
            var key = (type, side);

            if (_cache.TryGetValue(key, out var stampAnim))
                return stampAnim;

            var parent = side == StampSide.My ? _myRoot : _opponentRoot;

            stampAnim = Instantiate(_stampAnimPrefabs[(int)type], parent);
            _cache.Add(key, stampAnim);
            return stampAnim;
        }
		
		private bool IsMoving(StampSide side)
		{
			return side == StampSide.My ? _isMyMoving : _isOpponentMoving;
		}
		
		private void SetMoving(StampSide side, bool value)
		{
			if (side == StampSide.My) _isMyMoving = value;
			else _isOpponentMoving = value;
		}
	}
}