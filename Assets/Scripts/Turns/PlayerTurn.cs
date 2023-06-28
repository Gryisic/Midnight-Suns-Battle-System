using System;
using Game;

namespace Turns
{
    public class PlayerTurn : Turn
    {
        private readonly Player _player;

        private bool _isActive;
        
        public override event Action Activated;
        public override event Action Ended;

        public PlayerTurn(Player player)
        {
            _player = player;
        }

        public override void Activate()
        {
            _isActive = true;
            
            Activated?.Invoke();
            
            ActivatePlayer();
        }

        public override void Deactivate()
        {
            _isActive = false;
            
            DeactivatePlayer();
        }

        public void ActivatePlayer() 
        {
            if (_isActive)
                _player.Activate();
        }

        public void DeactivatePlayer() 
        {
            if (_isActive)
                _player.Deactivate();
        }
    }
}