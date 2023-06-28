using System;
using System.Collections.Generic;
using Actions;
using Mouse.Interfaces;
using TargetSelection.Interfaces;
using Units;
using UnityEngine;
using Utils;

namespace TargetSelection
{
    public class TargetSelector 
    {
        private Enums.TargetSelectionType _targetSelectionType;
        private int _remainingTargets;
        private bool _inSelectionMode;
        private Affect _cashedAffect;
        
        private Queue<Unit> _targets = new Queue<Unit>();

        public event Action<Queue<Unit>> Selected;
        public event Action<Vector3> SelectedArea;
        
        public void Start(ITargetSelectionData targetSelectionData)
        {
            _targets.Clear();
            
            _targetSelectionType = targetSelectionData.TargetSelectionType;
            _remainingTargets = targetSelectionData.TargetsCount;

            _inSelectionMode = true;
        }

        public void Update(IMouseData mouseData)
        {
            if (_inSelectionMode == false)
                return;
            
            switch (_targetSelectionType)
            {
                case Enums.TargetSelectionType.Area:
                    _inSelectionMode = false;
                    SelectedArea?.Invoke(mouseData.Position);
                    return;
                
                case Enums.TargetSelectionType.Single:
                    _targets.Clear();
                    
                    if (TryEnqueueTarget(mouseData) == false)
                        return;
                    
                    _inSelectionMode = false;
                    Selected?.Invoke(_targets);
                    return;
                
                case Enums.TargetSelectionType.Chain:
                {
                    if (TryEnqueueTarget(mouseData) == false) 
                        return;
                    
                    _remainingTargets--;
                
                    if (_remainingTargets <= 0)
                    {
                        _inSelectionMode = false;
                        Selected?.Invoke(_targets);
                    }
                    break;
                }
            }
        }

        private bool TryEnqueueTarget(IMouseData mouseData)
        {
            if (mouseData.Target.TryGetComponent(out Unit target) == false) 
                return false;
            
            _targets.Enqueue(target);

            return true;
        }
    }
}