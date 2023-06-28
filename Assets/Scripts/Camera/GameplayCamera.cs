using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    public class GameplayCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineBrain _brain;
        [SerializeField] private CinemachineVirtualCamera _freeCamera;
        [SerializeField] private FollowCamera _followCamera;
        [SerializeField] private Anchor _anchor;

        private Plane _plane = new Plane(Vector3.up, 0);
        private Vector2 _mousePosition;
        
        private ActiveCamera _cashedCamera;

        public event Action<Vector3> MousePositionUpdated; 

        public void UpdateMousePosition(InputAction.CallbackContext callbackContext) 
        {
            _mousePosition = callbackContext.ReadValue<Vector2>();
            
            MousePositionUpdated?.Invoke(MouseToWorldPosition());
        }
        
        public void RotateToLeft(InputAction.CallbackContext callbackContext) => Rotate(-30);

        public void RotateToRight(InputAction.CallbackContext callbackContext) => Rotate(30);

        public void StopMoving(InputAction.CallbackContext callbackContext) => _anchor.StopMoving();

        public void StartMoving(InputAction.CallbackContext callbackContext)
        {
            Vector2 inputDirection = callbackContext.ReadValue<Vector2>();
            Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y);

            _anchor.StartMoving(direction);
        }

        public void StartFollowing(Transform transformToFollow)
        {
            _cashedCamera = _followCamera;
            
            _followCamera.LookAt(transformToFollow);
            
            _cashedCamera.Camera.Priority = 100;
        }
        
        public void StopFollowing()
        {
            _cashedCamera.RestoreDefault();
        }

        public Vector3 MouseToWorldPosition()
        {
            Vector3 worldPosition = Vector3.zero;
            Ray ray = _brain.OutputCamera.ScreenPointToRay(_mousePosition);

            if (_plane.Raycast(ray, out float distance))
                worldPosition = ray.GetPoint(distance);
            
            return worldPosition;
        }
        
        private void Rotate(float angle)
        {
            Vector3 finalRotation = _anchor.Transform.rotation.eulerAngles + new Vector3(0, angle, 0);

            _anchor.Transform.DORotate(finalRotation, 0.5f);
        }
    }
}