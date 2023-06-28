using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Camera
{
    public class Anchor : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;

        private CancellationTokenSource _moveTokenSource;
        
        public Transform Transform => _transform;

        public void StartMoving(Vector3 direction)
        {
            _moveTokenSource = new CancellationTokenSource();
            
            MoveAsync(direction).Forget();   
        }

        public void StopMoving()
        {
            _moveTokenSource.Cancel();
            _moveTokenSource.Dispose();
        }

        private Vector3 Velocity(Vector3 direction)
        {
            Vector3 velocity = Vector3.zero;
            
            if (direction.z > 0)
                velocity += _transform.forward * _speed;
            if (direction.x > 0)
                velocity += _transform.right * _speed;
            if (direction.z < 0)
                velocity += _transform.forward * -1f * _speed;
            if (direction.x < 0)
                velocity += _transform.right * -1f * _speed;

            return velocity;
        }
        
        private async UniTask MoveAsync(Vector3 direction)
        {
            while (_moveTokenSource.IsCancellationRequested == false)
            {
                Vector3 velocity = Velocity(direction);
                
                _rigidbody.velocity = velocity;

                await UniTask.WaitForFixedUpdate();
            }

            _rigidbody.velocity = Vector3.zero;
        }
    }
}