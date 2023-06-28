using System;
using System.Collections.Generic;
using DG.Tweening;
using Units.Interfaces;
using UnityEngine;
using Utils;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI : UIElement, IDisposable
    {
        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private LocalUI _localUI;
        [SerializeField] private UIElement[] _uiElements;

        private Dictionary<Type, UIElement> _typeElementMap = new Dictionary<Type, UIElement>();

        private void Awake()
        {
            if (_canvas == null)
            {
                Debug.LogWarning("Canvas Group of UI isn't setted");

                _canvas = GetComponent<CanvasGroup>();
            }

            foreach (var element in _uiElements)
            {
                _typeElementMap.Add(element.GetType(), element);
            }
        }

        public void Initialize(IUnitData initialData, UnityEngine.Camera camera)
        {
            UpdateUnitData(Enums.UnitDataType.Full, initialData);
            _localUI.Initialize(camera);
        }
        
        public override void Activate()
        {
            _localUI.Activate();
            
            foreach (var element in _uiElements)
            {
                element.Activate();
            }
        }

        public override void Deactivate()
        {
            _localUI.Deactivate();
            
            foreach (var element in _uiElements)
            {
                element.Deactivate();
            }
        }
        
        public void Dispose()
        {
            _localUI.Dispose();
            
            foreach (var element in _uiElements)
            {
                if (element is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        public void Hide()
        {
            DOTween.Sequence().
                Append(_canvas.DOFade(0, Constants.UIFadeDuration).From(1)).
                AppendCallback(Deactivate).
                Play();
        }

        public void Show()
        {
            DOTween.Sequence().
                AppendCallback(Activate).
                Append(_canvas.DOFade(1, Constants.UIFadeDuration).From(0)).
                Play();
        }

        public void UpdateUnitData(Enums.UnitDataType type, IUnitData unitData)
        {
            UnitDataView view = _typeElementMap[typeof(UnitDataView)] as UnitDataView;
            view.UpdateData(type, unitData);
        }
        
        public void UpdateRuleValue(Enums.RuleType type, int value)
        {
            RulesView view = _typeElementMap[typeof(RulesView)] as RulesView;
            view.UpdateAmount(type, value);
        }
    }
}