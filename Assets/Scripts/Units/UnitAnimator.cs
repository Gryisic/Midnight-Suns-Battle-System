using System;
using UnityEngine;
using Utils;

namespace Units
{
    [Serializable]
    public class UnitAnimator
    {
        [SerializeField] private Animator _animator;

        public void PlayStandardAnimation(Enums.StandardAnimationType animationType)
        {
            switch (animationType)
            {
                case Enums.StandardAnimationType.Idle:
                    Reset();
                    break;
                
                case Enums.StandardAnimationType.Run:
                    SetBool("Run", true);
                    break;
                
                case Enums.StandardAnimationType.TakeDamage:
                    SetTrigger("TakeDamage");
                    break;
            }
        }

        private void SetBool(string name, bool value) => _animator.SetBool(name, value);

        private void SetTrigger(string name) => _animator.SetTrigger(name);

        private void Reset()
        {
            AnimatorControllerParameter[] parameters = _animator.parameters;

            foreach (var parameter in parameters)
            {
                switch (parameter.type)
                {
                    case AnimatorControllerParameterType.Bool:
                        _animator.SetBool(parameter.nameHash, parameter.defaultBool);
                        break;
                    
                    case AnimatorControllerParameterType.Float:
                        _animator.SetFloat(parameter.nameHash, parameter.defaultFloat);
                        break;
                    
                    case AnimatorControllerParameterType.Int:
                        _animator.SetInteger(parameter.nameHash, parameter.defaultInt);
                        break;
                    
                    case AnimatorControllerParameterType.Trigger:
                        _animator.ResetTrigger(parameter.nameHash);
                        break;
                }
            }
        }
    }
}