using System;

namespace Turns
{
    public abstract class Turn
    {
        public abstract event Action Activated;
        public abstract event Action Ended;
        
        public abstract void Activate();

        public abstract void Deactivate();
    }
}