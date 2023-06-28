using System;
using System.Threading;
using Cards.Interfaces;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDisposable
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _type;
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _background;

        [SerializeField] private Color _attackColor;
        [SerializeField] private Color _skillColor;

        [SerializeField] private float _inHandYPosition;
        [SerializeField] private float _previewYPosition;

        private readonly UIAnimator _animator = new UIAnimator();

        private CancellationTokenSource _tokenSource; 
        private bool _canBeSelected = true;
        private bool _isSelected;
        private bool _isDisposed;
        
        public event Action<int> Selected;
        public event Action<int> Hovered;
        public event Action HoverReleased; 

        private float LockedPosition => _inHandYPosition * 2f;
        private int Index => transform.GetSiblingIndex();
        private RectTransform RectTransform => transform as RectTransform;

        private float SelectedPosition => (Mathf.Abs(_inHandYPosition) + Mathf.Abs(_previewYPosition)) / 4;

        public void Activate()
        {
            _isDisposed = false;
            
            _tokenSource = new CancellationTokenSource();
            
            ActivateAsync().Forget();
        }

        public void Deactivate() => gameObject.SetActive(false);
        
        public void Dispose()
        {
            if (_isDisposed) 
                return;
            
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _animator.SlideY(RectTransform, _previewYPosition, 0.2f);

            Hovered?.Invoke(Index);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            float finalPosition = _inHandYPosition;

            if (_isSelected)
                finalPosition = SelectedPosition;
            else if (_canBeSelected == false)
                finalPosition = LockedPosition;
            
            _animator.SlideY(RectTransform, finalPosition, 0.2f);
            
            HoverReleased?.Invoke();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isSelected || _canBeSelected == false)
                return;
            
            _isSelected = true;
                
            _animator.SlideY(RectTransform, SelectedPosition, 0.2f);
                
            Selected?.Invoke(Index);
        }

        public void UpdateData(ICardDataProvider dataProvider)
        {
            _name.text = dataProvider.Name;
            _description.text = dataProvider.Description;
            _type.text = $"{dataProvider.SkillType}";
            _cost.text = dataProvider.Cost.ToString();
            _icon.sprite = dataProvider.Icon;
            _background.color = BackgroundColor(dataProvider.SkillType);
        }

        public void Deselect()
        {
            _animator.SlideY(RectTransform, _inHandYPosition, 0.2f);
                
            _isSelected = false;
        }

        public void LockSelection()
        {
            _canBeSelected = false;

            _animator.SlideY(RectTransform, LockedPosition, 0.2f);
        }

        public void UnlockSelection()
        {
            _canBeSelected = true;

            _animator.SlideY(RectTransform, _inHandYPosition, 0.2f);
        }

        private Color BackgroundColor(Enums.SkillType skillType)
        {
            switch (skillType)
            {
                case Enums.SkillType.Heroic:
                    return _attackColor;
                
                case Enums.SkillType.Attack:
                    return _attackColor;
                
                case Enums.SkillType.Skill:
                    return _skillColor;
            }

            throw new ArgumentException($"Background color for {skillType} is not defined");
        }

        private async UniTask ActivateAsync()
        {
            _canvasGroup.alpha = 0;
            gameObject.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(Constants.UIFadeDuration), cancellationToken: _tokenSource.Token);

            Vector2 initialPosition = RectTransform.anchoredPosition;
            Vector2 animationPosition = new Vector2(initialPosition.x + 250f, _inHandYPosition + 300f);
            RectTransform.anchoredPosition = animationPosition;
            
            Vector2 position = new Vector2(initialPosition.x, _inHandYPosition);
            Vector3 rotation = new Vector3(0f, 0f, 1080f);

            Sequence sequence = DOTween.Sequence().
                Append(_canvasGroup.DOFade(1, 0.2f)).
                AppendInterval(0.3f).
                Append(RectTransform.DOJumpAnchorPos(position, 225, 1, 0.5f)).
                Join(RectTransform.DORotate(rotation, 0.5f, RotateMode.FastBeyond360));
            
            _animator.PlayCustomSequence(sequence);
            
            _tokenSource.Dispose();
            _isDisposed = true;
        }
    }
}