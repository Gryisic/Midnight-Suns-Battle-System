using System;
using System.Collections.Generic;
using System.Linq;
using Arena;
using Mouse.Interfaces;
using Turns.Interfaces;
using UnityEngine;
using Utils;

namespace Mouse
{
    public class Mouse
    {
        private readonly MouseData _data = new MouseData();
        private readonly Queue<MouseState> _statesIndention = new Queue<MouseState>();
        private readonly MouseState[] _states;

        private MouseState _currentState;
        private bool _explicitlyChanged;
        
        public event Func<Vector3> RequestPosition;
        public event Func<IRulesData> RequestRulesData; 
        public event Func<PointerTarget> RequestTarget; 
        public event Action<IMouseData> StateChanged;
        public event Action<IMouseData> TargetSelection; 

        public Mouse()
        {
            _states = new MouseState[] {
                new FreeState(),
                new MoveState(),
                new SelectionState(),
                new SelectedState()
            };
        }

        public void Select()
        {
            if (RequestPosition == null)
                throw new NullReferenceException("No one subscribed to 'Request Position' event");
            
            Vector3 position = RequestPosition.Invoke();
            _data.UpdatePosition(position);

            PointerTarget target = RequestTarget?.Invoke();
            _data.UpdateTarget(target);

            MouseState nextState = DefineState(_data.State);
            
            if (nextState is SelectionState)
                TargetSelection?.Invoke(_data);
            
            if (nextState == _currentState) 
                return;
            
            if (nextState is FreeState == false)
                _statesIndention.Enqueue(nextState);

            ChangeState(nextState);
        }

        public void Deselect()
        {
            MouseState nextState = _states.First(s => s is FreeState);

            if (_statesIndention.Count > 1)
            {
                nextState = _statesIndention.Dequeue();
                nextState = _statesIndention.Dequeue();
            }
            else
            {
                _statesIndention.Clear();
            }
            
            ChangeState(nextState);
        }

        public void ResetState()
        {
            _statesIndention.Clear();

            MouseState nextState = _states.First(s => s is FreeState);
            ChangeState(nextState);
        }

        public void ExplicitlyChangeStateToSelected() 
        {
            ChangeState(_states.First(s => s is SelectedState));

            _explicitlyChanged = true;
        }

        private void ChangeState(MouseState nextState) 
        {
            if (_explicitlyChanged == false)
            {
                _currentState?.Exit();
                _currentState = nextState;
                _currentState.Enter();
            
                _data.UpdateState(_currentState.State);
            
                StateChanged?.Invoke(_data);
            }
            else
            {
                _explicitlyChanged = false;
            }
        }

        private MouseState DefineState(Enums.MouseState state)
        {
            MouseState nextState = _currentState;
            IRulesData rulesData = RequestRulesData?.Invoke();
            
            switch (state)
            {
                case Enums.MouseState.Free:
                    if (_data.Target != null)
                    {
                        if (_data.Target.PointerTargetType == Enums.PointerTargetType.Floor && rulesData.Moves > 0)
                            nextState = _states.First(s => s is MoveState);
                        else if (_data.Target.PointerTargetType == Enums.PointerTargetType.Interactable ||
                                 _data.Target.PointerTargetType == Enums.PointerTargetType.UI && rulesData.CardPlays > 0)
                            nextState = _states.First(s => s is SelectionState);
                    }
                    break;
                
                case Enums.MouseState.Move:
                    nextState = _data.Target.PointerTargetType == Enums.PointerTargetType.Floor ? 
                        _states.First(s => s is SelectedState) : 
                        _states.First(s => s is MoveState);
                    break;

                case Enums.MouseState.Selection:
                    break;
                
                case Enums.MouseState.Selected:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return nextState;
        }
    }
}