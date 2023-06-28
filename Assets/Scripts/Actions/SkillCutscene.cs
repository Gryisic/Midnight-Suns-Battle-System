using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Actions
{
    public class SkillCutscene : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private PlayableDirector _playableDirector;

        public event System.Action RequestSkillActivation;
        public event Action<int> RequestParticleActivation; 

        public string Name => _name;
        private Transform Transform => transform;
        private Transform _root;

        public void RaiseSkillActionEvent() => RequestSkillActivation?.Invoke();
        
        public void RaiseParticleEvent(int index) => RequestParticleActivation?.Invoke(index);
        
        public async UniTask PlayAsync(Transform initialPosition, CancellationToken token)
        {
            SetPositionAndRotation(initialPosition);
            _playableDirector.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(_playableDirector.duration), cancellationToken: token);
            
            Transform.SetParent(_root);
        }

        private void SetPositionAndRotation(Transform initialPosition)
        {
            _root = initialPosition;
            Transform.SetParent(null);
            
            Transform.position = initialPosition.position;
            Transform.rotation = initialPosition.rotation;
            
            TimelineAsset timelineAsset = _playableDirector.playableAsset as TimelineAsset;
            foreach (var track in timelineAsset.GetOutputTracks())
            {
                AnimationTrack animationTrack = track as AnimationTrack;
                if (animationTrack == null) 
                    continue;
                
                foreach (var timelineClip in animationTrack.GetClips())
                {
                    AnimationPlayableAsset animationAsset = timelineClip.asset as AnimationPlayableAsset;
                    if (animationAsset == null) 
                        continue;
                    
                    animationAsset.position = initialPosition.position;
                    animationAsset.rotation = initialPosition.rotation;
                }
            }
        }
    }
}