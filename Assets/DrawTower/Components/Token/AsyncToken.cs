using System;
using System.Threading;

namespace DrawTower.Token
{
	public abstract class AsyncToken : IDisposable
	{
		protected CancellationTokenSource _cancellationTokenSource;

		protected CancellationToken GetToken()
		{
			_cancellationTokenSource = new CancellationTokenSource();
			return _cancellationTokenSource.Token;
		}

		public virtual void Dispose()
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
