using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class HealthBarView
    {
        [SerializeField] private TextMeshProUGUI _currentAmount;
        [SerializeField] private TextMeshProUGUI _maxAmount;
        [SerializeField] private Bar _bar;
        [SerializeField] private Bar _background;

        public void UpdateAmount(int currentAmount, int maxAmount)
        {
            float dividedAmount = (float)currentAmount / (float)maxAmount;

            _bar.UpdateAmount(dividedAmount);
            _background.UpdateAmount(1f - 0.02f - dividedAmount);

            _currentAmount.text = currentAmount.ToString();
            _maxAmount.text = $"/{maxAmount.ToString()}";
        }
        
        [Serializable]
        private class Bar
        {
            [SerializeField] private Image _bar;
            [SerializeField] private Image _corners;

            public void UpdateAmount(float amount)
            {
                _bar.fillAmount = amount;
                _corners.fillAmount = amount;
            }
        }
    }
}