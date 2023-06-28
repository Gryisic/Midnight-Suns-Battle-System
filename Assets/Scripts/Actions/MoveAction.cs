using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Units;
using UnityEngine;
using Utils;

namespace Actions
{
    public class MoveAction : Action
    {
        private const float RotationSpeed = 0.4f;
        private const float MovementSpeed = 8f;
        
        private readonly Transform _transform;
        private readonly Vector3 _moveTo;
        private readonly UnitAnimator _unitAnimator;
        
        public MoveAction(Transform transformToMove, Vector3 moveTo, UnitAnimator unitAnimator)
        {
            _transform = transformToMove;
            _moveTo = moveTo;
            _unitAnimator = unitAnimator;
        }

        public override async UniTask ExecuteAsync(CancellationToken token)
        {
            float time = (_moveTo - _transform.position).magnitude / MovementSpeed;
            
            UniTask arrivingTask = DOTween.Sequence().
                Append(_transform.DOLookAt(_moveTo, RotationSpeed)).
                AppendCallback(() => _unitAnimator.PlayStandardAnimation(Enums.StandardAnimationType.Run)).
                Append(_transform.DOMove(_moveTo, time)).
                SetEase(Ease.Linear).
                ToUniTask(cancellationToken: token);

            await arrivingTask;
            
            _unitAnimator.PlayStandardAnimation(Enums.StandardAnimationType.Idle);
        }
    }
}