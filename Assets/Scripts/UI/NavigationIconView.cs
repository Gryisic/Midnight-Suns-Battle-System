using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NavigationIconView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _circle;

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);
        
        public void UpdateIcon(Sprite icon)
        {
            if (icon == null)
                throw new NullReferenceException("Trying to add null icon");

            _icon.sprite = icon;
        }

        public void UpdatePosition(Vector3 position)
        {
            Vector3 iconPosition = new Vector3(position.x, position.y + 1, position.z);
            Vector3 circlePosition = new Vector3(position.x, 0.1f, position.z);

            _icon.transform.position = iconPosition;
            _circle.transform.position = circlePosition;
        }
    }
}