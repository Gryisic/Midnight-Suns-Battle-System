using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Actions
{
    public class SkillAction : Action
    {
        private readonly Skill _skill;
        private readonly Transform _transform;

        public SkillAction(Skill skill, Transform transform)
        {
            _skill = skill;
            _transform = transform;
        }

        public override async UniTask ExecuteAsync(CancellationToken token)
        {
            _skill.Cutscene.RequestSkillActivation += _skill.Execute;
            
            UniTask task = _skill.Cutscene.PlayAsync(_transform, token);

            await task;
            
            _skill.Cutscene.RequestSkillActivation -= _skill.Execute;
        }
    }
}