using Utils;

namespace Mouse
{
    public class MoveState : MouseState
    {
        public override Enums.MouseState State { get; protected set; }
        
        public override void Enter()
        {
            State = Enums.MouseState.Move;
        }

        public override void Exit()
        {
            
        }
    }
}