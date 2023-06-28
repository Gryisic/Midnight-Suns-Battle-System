using UnityEngine;

namespace UI
{
    public abstract class UIElement : MonoBehaviour
    {
        public abstract void Activate();
        
        public abstract void Deactivate();
    }
}