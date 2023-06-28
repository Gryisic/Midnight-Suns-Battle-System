using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Interfaces;
using Mouse.Interfaces;
using Turns.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class HandView : UIElement, IDisposable
    {
        [SerializeField] private HorizontalLayoutGroup _layoutGroup;
        [SerializeField] private List<CardView> _cards;

        private const int DefaultSpacing = 90;
        
        private int _lastCardIndex = -1;
        private int _lastSelectedCardIndex = -1;

        private bool _isLockedState;

        public Func<IRulesData> RequestRulesData;
        public event Action<int> CardSelected;

        public bool IsCardHovered { get; private set; }
        public int CoveredCardIndex { get; private set; }

        private void Awake()
        {
            if (_layoutGroup == null)
                _layoutGroup = GetComponent<HorizontalLayoutGroup>();

            if (_cards.Count < Constants.MaxAmountCardsInHand)
                _cards = GetComponentsInChildren<CardView>(true).ToList();

            _layoutGroup.spacing = DefaultSpacing;
        }

        public override void Activate()  
        {
            gameObject.SetActive(true);
            RefreshLayout();
        }

        public override void Deactivate() => gameObject.SetActive(false);

        public void Initialize()
        {
            _cards.ForEach(c =>
            {
                c.Selected += OnCardSelected;
                c.Hovered += OnCardHovered;
                c.HoverReleased += OnCardHoverReleased;
            });
        }

        public void Dispose()
        {
            _cards.ForEach(c =>
            {
                c.Selected -= OnCardSelected;
                c.Hovered -= OnCardHovered;
                c.HoverReleased -= OnCardHoverReleased;
                c.Dispose();
            });
        }

        public void UpdateCardsState(IMouseData mouseData)
        {
            IRulesData rulesData = RequestRulesData?.Invoke();

            if (rulesData.CardPlays <= 0 && _isLockedState == false || mouseData.State == Enums.MouseState.Move && _isLockedState == false)
            {
                _isLockedState = true;
                
                _cards.ForEach(c => c.LockSelection());
            }
            else if (rulesData.CardPlays > 0 && _isLockedState && mouseData.State != Enums.MouseState.Move)
            {
                _isLockedState = false;
                
                _cards.ForEach(c => c.UnlockSelection());
            }
        }
        
        public void Add(ICardDataProvider dataProvider)
        {
            if (_lastCardIndex >= Constants.MaxAmountCardsInHand)
                throw new InvalidOperationException("Number of cards in view and in hand are not equal");
            
            _lastCardIndex++;
            _cards[_lastCardIndex].UpdateData(dataProvider);
            _cards[_lastCardIndex].Activate();
            
            RefreshLayout();
        }

        public void Remove(int index)
        {
            if (_cards.Count - 1 < 0)
                throw new InvalidOperationException("Number of cards in view and in hand are not equal");

            CardView card = _cards[index];
            card.Deactivate();
            ResetSelection();

            _lastCardIndex--;
            
            Refresh(index);
        }

        public void ResetSelection()
        {
            if (_lastSelectedCardIndex >= 0)
                _cards[_lastSelectedCardIndex].Deselect();

            _lastSelectedCardIndex = -1;
        }

        private void OnCardSelected(int index)
        {
            ResetSelection();

            _lastSelectedCardIndex = index;
            
            CardSelected?.Invoke(index);
        }
        
        private void OnCardHovered(int index)
        {
            IsCardHovered = true;
            CoveredCardIndex = index;
        }

        private void OnCardHoverReleased()
        {
            IsCardHovered = false;
            CoveredCardIndex = -1;
        }

        private void Refresh(int removedCardIndex)
        {
            List<CardView> activeCards = _cards.Where(c => c.isActiveAndEnabled).ToList();
      
            for (var i = 0; i < activeCards.Count; i++)
            {
                if (i < removedCardIndex)
                    continue;

                CardView currentCard = _cards[i];

                _cards[i].transform.SetSiblingIndex(_cards.Count);

                _cards.Remove(currentCard);
                _cards.Add(currentCard);
                
                break;
            }
            
            RefreshLayout();
        }

        private void RefreshLayout()
        {
            _layoutGroup.enabled = true;
            _layoutGroup.spacing = DefaultSpacing - _lastCardIndex * 15;
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            
            _layoutGroup.enabled = false;
        }
    }
}