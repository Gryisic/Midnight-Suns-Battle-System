using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class UIAnimator
    {
        public void SlideY(RectTransform transform, float endValue, float duration) => transform.DOAnchorPosY(endValue, duration);

        public void PlayCustomSequence(Sequence sequence) => sequence.Play();
    }
}