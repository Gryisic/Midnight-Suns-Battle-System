using System;
using Mouse.Interfaces;
using UI;
using Units;
using Units.Interfaces;
using UnityEngine;
using Utils;

namespace Arena
{
    [Serializable]
    public class NavigationMarker
    {
        [SerializeField] private NavigationIconView _navigationIconView;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Material _strictMaterial;
        [SerializeField] private Material _dashedMaterial;

        private Transform _lineOrigin;

        private Enums.NavigationLineType _currentLineType = Enums.NavigationLineType.None;
        private bool _isActive;
        private bool _isSticky;

        public event Func<Vector3> RequestMousePosition;
        public event Func<PointerTarget> RequestUnitData;

        public void Initialize(IUnitData unitData) => _lineOrigin = unitData.Transform;

        public void UpdateMarkerState(IMouseData mouseData)
        {
            if (mouseData.State == Enums.MouseState.Move || mouseData.State == Enums.MouseState.Selection)
                Activate(Enums.NavigationLineType.Dash);
            else
                Deactivate();
        }

        public void UpdateMarkerData(IUnitData unitData)
        {
            _lineOrigin = unitData.Transform;
            _navigationIconView.UpdateIcon(unitData.Icon);
            
            _lineRenderer.SetPosition(0, _lineOrigin.position);
        }
        
        public void UpdateMarker(Vector3 position)
        {
            if (_isActive == false) 
                return;

            PointerTarget unitData = RequestUnitData?.Invoke();
            Vector3 iconPosition = position;

            if (unitData != null && unitData.PointerTargetType == Enums.PointerTargetType.Unit)
            {
                IUnitData data = unitData.gameObject.GetComponent<Unit>();

                _isSticky = true;

                position = data.Transform.position;
                iconPosition = new Vector3(position.x, position.y + 1, position.z);
            }
            else
            {
                _isSticky = false;
            }
            
            UpdateMaterial();

            _lineRenderer.SetPosition(1, position);
            _navigationIconView.UpdatePosition(iconPosition);
        }

        private void Activate(Enums.NavigationLineType lineType)
        {
            _isActive = true;
            
            if (lineType != _currentLineType)
                UpdateMaterial();

            if (RequestMousePosition == null)
                throw new NullReferenceException("No one subscribed to 'Request Position' event");
            
            Vector3 endPosition = RequestMousePosition.Invoke();
            
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _lineOrigin.position);
            _lineRenderer.SetPosition(1, endPosition);
            
            _navigationIconView.Activate();
            _navigationIconView.UpdatePosition(endPosition);
        }

        private void Deactivate()
        {
            _isActive = false;
            
            _navigationIconView.Deactivate();
            
            _currentLineType = Enums.NavigationLineType.None;
            
            _lineRenderer.positionCount = 1;
        }

        private void UpdateMaterial()
        {
            if (_isSticky)
            {
                _lineRenderer.material = _strictMaterial;
                _currentLineType = Enums.NavigationLineType.Strict;
            }
            else
            {
                _lineRenderer.material = _dashedMaterial;
                _currentLineType = Enums.NavigationLineType.Dash;
            }
        }
    }
}