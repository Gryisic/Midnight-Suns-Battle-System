using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class RulesView : UIElement
    {
        [SerializeField] private HeroismView _heroismView;
        [SerializeField] private RuleView _cardPlays;
        [SerializeField] private RuleView _redraws;
        [SerializeField] private RuleView _moves;
        
        public override void Activate() => gameObject.SetActive(true);

        public override void Deactivate() => gameObject.SetActive(false);

        public void UpdateAmount(Enums.RuleType type, int amount)
        {
            switch (type)
            {
                case Enums.RuleType.CardPlays:
                    _cardPlays.UpdateAmount(amount);
                    break;
                
                case Enums.RuleType.Redraws:
                    _redraws.UpdateAmount(amount);
                    break;
                
                case Enums.RuleType.Moves:
                    _moves.UpdateAmount(amount);
                    break;
                
                case Enums.RuleType.Heroism:
                    _heroismView.UpdateAmount(amount);
                    break;
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        [Serializable]
        private class RuleView
        {
            [SerializeField] private TextMeshProUGUI _amount;
            [SerializeField] private HorizontalLayoutGroup _layout;
            [SerializeField] private List<Image> _sectors;

            public void UpdateAmount(int amount)
            {
                _amount.text = amount.ToString();

                for (int i = 0; i < _sectors.Count; i++)
                    _sectors[i].gameObject.SetActive(i < amount);
            }
        }
    }
}