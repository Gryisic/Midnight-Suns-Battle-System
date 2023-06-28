using System;
using System.Collections.Generic;
using Arena;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils
{
    [Serializable]
    public class ViewAndPointerEventsHandler
    {
        [SerializeField] private EventSystem _eventSystem;

        private Enums.MouseState _currentState;

        public bool IsMouseOverUI() 
        {
            return _eventSystem.IsPointerOverGameObject();
        }

        public PointerTarget PointerTarget()
        {
            PointerEventData pointerEventData = new PointerEventData(_eventSystem)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            _eventSystem.RaycastAll(pointerEventData, raycastResults);
            
            foreach (var result in raycastResults)
            {
                if (result.gameObject.TryGetComponent(out PointerTarget pointerTarget))
                {
                    return pointerTarget;
                }
            }

            return null;
        }
    }
}