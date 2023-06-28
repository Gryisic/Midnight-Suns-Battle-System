using System;
using Game;
using Units;
using UnityEngine.InputSystem;

namespace Turns
{
    public class TurnsHandler : IDisposable
    {
        private Turn _activeTurn;

        public PlayerTurn PlayerTurn { get; private set; }
        public EnemyTurn EnemyTurn { get; private set; }

        public void Initialize(Player player, UnitsHandler unitsHandler)
        {
            PlayerTurn = new PlayerTurn(player);
            EnemyTurn = new EnemyTurn(unitsHandler);

            EnemyTurn.Initialize();
            
            PlayerTurn.Ended += ChangeTurn;
            EnemyTurn.Ended += ChangeTurn;
        }
        
        public void Dispose()
        {
            PlayerTurn.Ended -= ChangeTurn;
            EnemyTurn.Ended -= ChangeTurn;
            
            EnemyTurn.Dispose();
        }

        public void Start()
        {
            _activeTurn = PlayerTurn;
            _activeTurn.Activate();
        }

        public void ChangeTurnManually(InputAction.CallbackContext callbackContext)
        {
            if (_activeTurn is PlayerTurn)
                ChangeTurn();
        }

        private void ChangeTurn()
        {
            _activeTurn.Deactivate();

            _activeTurn = _activeTurn is PlayerTurn ? EnemyTurn as Turn : PlayerTurn as Turn;
            
            _activeTurn.Activate();
        }
    }
}