using UnityEngine;
using R3;

namespace DrawTower.Object
{
    public class KillZone : MonoBehaviour, IKillZone
    {
        private readonly Subject<bool> _onBlockOutOfBounds = new();
        public Observable<bool> OnBlockOutOfBounds() => _onBlockOutOfBounds;

        /// <summary>
        /// 落下したブロックを検知するキルゾーン用コリジョン（最初の1回だけ通知）
        /// </summary>
        private void OnCollisionEnter(Collision collision)
        {           
            var onlineBlock = collision.gameObject.GetComponent<OnlineBlock>();
            if (onlineBlock != null)
            {
                _onBlockOutOfBounds.OnNext(onlineBlock.Object.HasStateAuthority);
            }
            
            var offlineBlock = collision.gameObject.GetComponent<OfflineBlock>();
            if (offlineBlock != null)
            {
                _onBlockOutOfBounds.OnNext(true);
            }
        }
    }
}