using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Units;

namespace Turns
{
    public class EnemyTurn : Turn, IDisposable
    {
        private readonly UnitsHandler _unitsHandler;

        private CancellationTokenSource _actTokenSource;
        
        private bool _isActive;
        private bool _isActEnded;
        
        public override event Action Activated;
        public override event Action Ended;

        public EnemyTurn(UnitsHandler unitsHandler)
        {
            _unitsHandler = unitsHandler;
        }

        public void Initialize() => _unitsHandler.Enemies.ForEach(e => e.UpdateTarget(_unitsHandler));
        
        public override void Activate()
        {
            _isActive = true;
            
            Activated?.Invoke();
            
            EmulateAsync().Forget();
        }

        public override void Deactivate()
        {
            _isActive = false;
        }
        
        public void Dispose()
        {
            if (_isActive)
                _actTokenSource.Cancel();
            _actTokenSource?.Dispose();
        }

        public void OnUnitActed() 
        {
            if (_isActive)
                _isActEnded = true;
        }

        private async UniTask EmulateAsync()
        {
            _actTokenSource = new CancellationTokenSource();
            
            foreach (var enemy in _unitsHandler.Enemies)
            {
                _isActEnded = false;
                
                enemy.UseRandomSkill();
                enemy.Act();
                
                await UniTask.WaitUntil(() => _isActEnded, cancellationToken: _actTokenSource.Token);
            }

            _unitsHandler.Enemies.ForEach(e => e.UpdateTarget(_unitsHandler));
            
            _actTokenSource.Cancel();
            
            Ended?.Invoke();
        }
    }
}