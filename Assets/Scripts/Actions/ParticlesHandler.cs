using System;
using UnityEngine;

namespace Actions
{
    [Serializable]
    public class ParticlesHandler
    {
        [SerializeField] private ParticleTransformMap[] _particleSystems;

        public event Action<ParticleTransformMap> SetOnTarget; 

        public void PlayParticle(int index)
        {
            if (index < 0)
                throw new ArgumentException("Index is less then zero");

            if (_particleSystems.Length <= index) 
                throw new ArgumentException("Index is outside of array bounds");

            ParticleTransformMap particleTransformMap = _particleSystems[index];
            ParticleSystem particleSystem = particleTransformMap.ParticleSystem;
            
            if (particleTransformMap.SetOnTarget)
                SetOnTarget?.Invoke(particleTransformMap);
            
            particleTransformMap.SetPositionToTransform();
            particleSystem.gameObject.SetActive(true);
            particleSystem.Play();
        }
    }
}