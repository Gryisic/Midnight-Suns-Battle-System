using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LocalHealthBarView : LocalUIElement
    {
        [SerializeField] private Image _bar;
        
        public override void Activate() => gameObject.SetActive(true);

        public override void Deactivate() => gameObject.SetActive(false);

        public void UpdateAmount(int currentAmount, int maxAmount)
        {
            float dividedValue = (float)currentAmount / (float)maxAmount;

            LerpAmountAsync(dividedValue).Forget();
        }

        private async UniTask LerpAmountAsync(float amount)
        {
            float timer = 0;

            while (timer < 0.5f)
            {
                _bar.fillAmount = Mathf.Lerp(_bar.fillAmount, amount, Time.fixedDeltaTime * 10);
                
                await UniTask.WaitForFixedUpdate();
                timer += Time.fixedDeltaTime;
            }
        }
    }
}