using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyTargetIconView : LocalUIElement
    {
        [SerializeField] private Image _icon;
        
        public override void Activate() => gameObject.SetActive(true);

        public override void Deactivate() => gameObject.SetActive(false);

        public void UpdateIcon(Sprite icon) => _icon.sprite = icon;
    }
}