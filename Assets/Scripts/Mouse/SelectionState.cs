using Utils;

namespace Mouse
{
    public class SelectionState : MouseState
    {
        public override Enums.MouseState State { get; protected set; }
        
        public override void Enter()
        {
            State = Enums.MouseState.Selection;
        }

        public override void Exit()
        {
            
        }
    }
}