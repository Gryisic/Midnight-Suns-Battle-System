using Utils;

namespace Mouse
{
    public class SelectedState : MouseState
    {
        public override Enums.MouseState State { get; protected set; }
        
        public override void Enter()
        {
            State = Enums.MouseState.Selected;
        }

        public override void Exit()
        {
            
        }
    }
}