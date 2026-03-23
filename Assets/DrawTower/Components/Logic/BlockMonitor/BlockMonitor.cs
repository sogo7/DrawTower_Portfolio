using DrawTower.Token;
using System.Collections.Generic;
using UnityEngine;
using R3;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace DrawTower.Logic
{
    public class BlockMonitor : AsyncTokenMonoBehaviour, IBlockMonitor
    {
        private readonly List<Transform> _blocks = new();
        private float _cameraOffsetY = 7f;      
        private float _highestY = 0f;
        private Tween _tween;

        /// <summary>
        /// ブロックを監視リストに登録し、
        /// カメラ追従・落下チェックを行う
        /// </summary>
        public async UniTask TrackBlockAsync(Transform block, CancellationToken ct)
        {
            if (block == null) return;
            if (!_blocks.Contains(block))
                _blocks.Add(block);
                
            UpdateHighestBlock();
            await MoveCameraAsync(ct);
            await CheckOutOfBoundsAsync(ct);
        }
        
        public float GetSpawnPosY() => _highestY == 0 ? _highestY : _highestY + _cameraOffsetY;

        /// <summary>
        /// 現在登録されているブロックの中で
        /// 一番高い位置を検出し、_highestYに記録
        /// </summary>
        private void UpdateHighestBlock()
        {
            float maxY = float.MinValue;
            bool anyValid = false;

            foreach (var b in _blocks)
            {
                if (b == null) continue;
                anyValid = true;
                if (b.position.y > maxY)
                    maxY = b.position.y;
            }

            _highestY = anyValid ? maxY : 0f;
        }

        /// <summary>
        /// カメラを最高点＋オフセット位置まで移動させる
        /// </summary>
        private async UniTask MoveCameraAsync(CancellationToken ct)
        {
            var mainCamera = Camera.main;
            float targetY = _highestY + _cameraOffsetY;
            _tween?.Kill();

            _tween = mainCamera.transform.DOMoveY(targetY, 0.8f)
                .SetEase(Ease.OutSine)
                .SetUpdate(UpdateType.Late);

            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
        }
        
        /// <summary>
        /// 動いているブロックがある限り、そのブロックの高さを監視する
        /// 全ブロック停止で監視終了
        /// 落下ラインを超えたらイベント発火
        /// </summary>
        private async UniTask CheckOutOfBoundsAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                bool allStopped = true;

                foreach (var b in _blocks)
                {
                    if (b == null) continue;

                    var rb = b.GetComponent<Rigidbody>();
                    if (rb == null) continue;

                    float speed = rb.linearVelocity.magnitude;
                    float angular = rb.angularVelocity.magnitude;

                    // 動いているブロックを監視
                    if (speed >= 0.05f || angular >= 0.05f)
                    {
                        allStopped = false;
                    }
                }

                if (allStopped)
                    return;

                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            _blocks.Clear();
        }
    }
}
