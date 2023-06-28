using UnityEngine;
using Utils;

namespace UI
{
    public abstract class ActiveUI : UIElement
    {
        [SerializeField] private Enums.ActiveUIType _uiType;
    }
}