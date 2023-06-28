using Arena;
using UnityEngine;
using Utils;

namespace Mouse.Interfaces
{
    public interface IMouseData
    {
        Vector3 Position { get; }
        Enums.MouseState State { get; }
        PointerTarget Target { get; }
    }
}