using Utils;

namespace Mouse
{
    public class FreeState : MouseState
    {
        public override Enums.MouseState State { get; protected set; }
        
        public override void Enter()
        {
            State = Enums.MouseState.Free;
        }

        public override void Exit()
        {
            
        }
    }
}