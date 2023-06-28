using Arena;
using Mouse.Interfaces;
using UnityEngine;
using Utils;

namespace Mouse
{
    public class MouseData : IMouseData
    {
        public Vector3 Position { get; private set; }
        public Enums.MouseState State { get; private set; }
        public PointerTarget Target { get; private set; }

        public void UpdatePosition(Vector3 position) => Position = position;
        
        public void UpdateState(Enums.MouseState state) => State = state;
        
        public void UpdateTarget(PointerTarget target) => Target = target;
    }
}