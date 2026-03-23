using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace DrawTower.Logic
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadOfflineGameSceneAsync(CancellationToken token = default) => await LoadSceneAsync("OfflineGame", token);
        
        private static async UniTask LoadSceneAsync(string sceneName, CancellationToken token = default)
        {
            var ao = SceneManager.LoadSceneAsync(sceneName);
            while (!ao.isDone && !token.IsCancellationRequested)
            {
                await UniTask.Yield(token);
            }
        }
    }
}
