using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Units.Interfaces;

namespace Actions
{
    public class ActionsExecutor
    {
        public event System.Action ExecutionStarted;
        public event System.Action ExecutionCompleted;

        private readonly Queue<Action> _actions = new Queue<Action>();

        private CancellationTokenSource _tokenSource;
        private bool _isExecuting;
        
        public IUnitData Sender { get; private set; }

        public void StopExecution() => _tokenSource.Cancel();

        public void Add(IUnitData sender, Action action)
        {
            if (_isExecuting)
                throw new InvalidOperationException("Trying to add new action while other actions executing");

            Sender = sender;
            
            _actions.Enqueue(action);
        }

        public void Execute()
        {
            if (_actions.Count <= 0)
                throw new InvalidOperationException("Trying to execute actions before adding any");
            
            _tokenSource = new CancellationTokenSource();
            
            ExecuteAsync().Forget();
        }

        private async UniTask ExecuteAsync()
        {
            ExecutionStarted?.Invoke();
            
            _isExecuting = true;

            while (_tokenSource.IsCancellationRequested == false && _actions.Count > 0)
                await _actions.Dequeue().ExecuteAsync(_tokenSource.Token);
            
            _tokenSource.Dispose();
            _isExecuting = false;
            
            ExecutionCompleted?.Invoke();
        }
    }
}