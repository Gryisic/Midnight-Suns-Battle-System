using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI
{
    public class LocalUIElement : UIElement, IDisposable
    {
        private UnityEngine.Camera _camera;
        private CancellationTokenSource _tokenSource;
        
        public override void Activate() => gameObject.SetActive(true);

        public override void Deactivate() => gameObject.SetActive(false);

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
        
        public void StartUpdating(UnityEngine.Camera camera)
        {
            _camera = camera;

            _tokenSource = new CancellationTokenSource();
            UpdateAsync().Forget();
        }

        public void StopUpdating() => _tokenSource.Cancel();

        private async UniTask UpdateAsync()
        {
            float delay = Time.deltaTime;
            
            while (_tokenSource.IsCancellationRequested == false)
            {
                transform.LookAt(transform.position + _camera.transform.forward);
                
                await UniTask.Delay(TimeSpan.FromSeconds(delay), delayTiming: PlayerLoopTiming.LastUpdate);
            }
            
            _tokenSource.Dispose();
        }
    }
}