using Utils;

namespace Mouse
{
    public abstract class MouseState
    {
        public abstract Enums.MouseState State { get; protected set; }

        public abstract void Enter();
        public abstract void Exit();
    }
}