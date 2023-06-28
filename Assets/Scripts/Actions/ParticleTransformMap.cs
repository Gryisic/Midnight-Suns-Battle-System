using System;
using UnityEngine;

namespace Actions
{
    [Serializable]
    public class ParticleTransformMap
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private bool _setOnTarget;

        public ParticleSystem ParticleSystem => _particleSystem;
        public bool SetOnTarget => _setOnTarget;
        public Transform Transform => _transform;
        
        public void SetPositionToTransform() => _particleSystem.transform.position = _transform.position;
    }
}