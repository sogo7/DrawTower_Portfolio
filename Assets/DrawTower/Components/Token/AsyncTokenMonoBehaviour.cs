using System;
using System.Threading;
using UnityEngine;

namespace DrawTower.Token
{
	public abstract class AsyncTokenMonoBehaviour : MonoBehaviour, IDisposable
	{
		protected CancellationTokenSource _cancellationTokenSource;

		protected CancellationToken GetToken()
		{
			_cancellationTokenSource = new CancellationTokenSource();
			return _cancellationTokenSource.Token;
		}
		
		protected virtual void OnDestroy()
		{
			Dispose();
		}

		public void Dispose()
		{
			if(_cancellationTokenSource != null)
			{
				_cancellationTokenSource?.Cancel();
				_cancellationTokenSource?.Dispose();
				_cancellationTokenSource = null;
			}
		}
	}
}