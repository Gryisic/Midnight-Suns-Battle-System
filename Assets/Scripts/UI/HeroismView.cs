using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class HeroismView : ActiveUI, IDisposable
    {
        private const string SegmentsAmountReference = "_DeactivatedSegments";
        private const float UpdateSpeed = 0.5f;
        
        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private Material _amountMaterial;
        
        private int _deactivatedSegmentsID;
        private int _activeAmount;
        private bool _isCounting;
        private CancellationTokenSource _tokenSource;

        public override void Activate() 
        {
            if (_deactivatedSegmentsID == 0)
                _deactivatedSegmentsID = Shader.PropertyToID(SegmentsAmountReference);
            
            gameObject.SetActive(true);
        }

        public override void Deactivate() => gameObject.SetActive(false);

        public void Dispose()
        {
            _amountMaterial.SetFloat(_deactivatedSegmentsID, Constants.MaxHeroismAmount);
            
            if (_isCounting)
                _tokenSource.Cancel();
            _tokenSource?.Dispose();
        }
        
        public void UpdateAmount(int amount)
        {
            _tokenSource = new CancellationTokenSource();

            UpdateAsync(amount).Forget();
        }

        private void UpdateValue(int amount)
        {
            _amount.text = amount.ToString();
            _amountMaterial.SetFloat(_deactivatedSegmentsID, Constants.MaxHeroismAmount - amount);
        }
        
        private async UniTask UpdateAsync(int amount)
        {
            if (amount == _activeAmount)
                return;
            
            _isCounting = true;
            
            await UniTask.Delay(TimeSpan.FromSeconds(Constants.UIFadeDuration), cancellationToken: _tokenSource.Token);
            await UniTask.WaitUntil(() => gameObject.activeSelf, cancellationToken: _tokenSource.Token);
            await UniTask.Delay(TimeSpan.FromSeconds(Constants.UIFadeDuration), cancellationToken: _tokenSource.Token);

            if (amount > _activeAmount)
                await IncreaseAsync(amount);
            else
                await DecreaseAsync(amount);

            _tokenSource.Cancel();
            _isCounting = false;
        }

        private async UniTask IncreaseAsync(int amount)
        {
            for (int i = _activeAmount; i <= amount; i++)
            {
                UpdateValue(i);

                await UniTask.Delay(TimeSpan.FromSeconds(UpdateSpeed), cancellationToken: _tokenSource.Token);
            }

            _activeAmount = amount;
        }
        
        private async UniTask DecreaseAsync(int amount)
        {
            for (int i = _activeAmount; i >= amount; i--)
            {
                UpdateValue(i);

                await UniTask.Delay(TimeSpan.FromSeconds(UpdateSpeed), cancellationToken: _tokenSource.Token);
            }
            
            _activeAmount = amount;
        }
    }
}