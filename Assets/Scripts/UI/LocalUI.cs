using System;
using UnityEngine;

namespace UI
{
    public class LocalUI : UIElement, IDisposable
    {
        [SerializeField] private LocalUIElement[] _elements;

        private UnityEngine.Camera _camera;
        
        public override void Activate() 
        {
            StartUpdating();
        }

        public override void Deactivate() 
        {
            StopUpdating();
        }

        public void Initialize(UnityEngine.Camera camera)
        {
            _camera = camera;
        }

        public void Dispose()
        {
            foreach (var localUIElement in _elements)
                localUIElement.Dispose();   
        }

        private void StartUpdating()
        {
            foreach (var localUIElement in _elements)
            {
                localUIElement.Activate();
                localUIElement.StartUpdating(_camera);
            }
        }
        
        private void StopUpdating()
        {
            foreach (var localUIElement in _elements)
            {
                localUIElement.StopUpdating();
                localUIElement.Deactivate();
            }
        }
    }
}