using System;
using System.Collections.Generic;
using Actions;
using Arena;
using Camera;
using Cards;
using TargetSelection;
using Turns;
using UI;
using Units;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Input = PlayerInput.Input;

namespace Game
{
    public class Game : MonoBehaviour, IDisposable
    {
        [SerializeField] private UnityEngine.Camera _mainCamera; 
        [SerializeField] private GameplayCamera _camera;
        [SerializeField] private Mock _mock;

        [Space] [SerializeField] private ViewAndPointerEventsHandler _viewAndPointerEventsHandler;
        [SerializeField] private HandView _handView;

        [Space] [SerializeField] private NavigationMarker _navigationMarker;

        [Space] [SerializeField] private UI.UI _ui;

        private Deck _deck;
        private Input _input;
        private Rules _rules;
        private Player _player;
        private Mouse.Mouse _mouse;
        private TargetSelector _targetSelector;
        private UnitsHandler _unitsHandler;
        private TurnsHandler _turnsHandler;
        private ActionsExecutor _actionsExecutor;

        private void Awake()
        {
            _input = new Input();
            _rules = new Rules();
            _player = new Player();
            _mouse = new Mouse.Mouse();
            _targetSelector = new TargetSelector();
            _unitsHandler = new UnitsHandler();
            _turnsHandler = new TurnsHandler();
            _actionsExecutor = new ActionsExecutor();

            Initialize();
            AttachInput();
            SubscribeToEvents();
            
            _mouse.ResetState();
            _turnsHandler.Start();
        }

        private void OnDestroy()
        {
            DeAttachInput();
            UnsubscribeToEvents();
        
            Dispose();
        }

        public void Dispose()
        {
            _player.Dispose();
            _unitsHandler.Dispose();
            _turnsHandler.Dispose();
            _deck.Dispose();
            _ui.Dispose();
            _handView.Dispose();
            _input?.Dispose();
        }

        private void Initialize()
        {
            InitializeUnits();

            _turnsHandler.Initialize(_player, _unitsHandler);
            _handView.Initialize();
            _navigationMarker.Initialize(_unitsHandler.Heroes[0]);
            _rules.Initialize();
            _ui.Initialize(_unitsHandler.Heroes[0], _mainCamera);

            InitializeDeck();
        }

        private void InitializeUnits()
        {
            _player.ChangeControllableUnit(_mock.PlayerUnit);
            _unitsHandler.Add(_mock.PlayerUnit);
            _mock.PlayerUnit.Initialize();

            foreach (var unit in _mock.Units)
            {
                _unitsHandler.Add(unit);
                unit.Initialize();
            }
        }

        private void InitializeDeck()
        {
            List<Card> cards = new List<Card>();

            _unitsHandler.Heroes.ForEach(h =>
            {
                cards.AddRange(h.CardsContainer.Cards);
            });

            _deck = new Deck(cards);
            _deck.Initialize();
        }
        
        private void AttachInput()
        {
            _input.Gameplay.RotateCameraToLeft.performed += _camera.RotateToLeft;
            _input.Gameplay.RotateCameraToRight.performed += _camera.RotateToRight;
            _input.Gameplay.MoveCamera.performed += _camera.StartMoving;
            _input.Gameplay.MoveCamera.canceled += _camera.StopMoving;
            _input.Gameplay.MousePosition.performed += _camera.UpdateMousePosition;
            _input.Gameplay.MouseClick.performed += Select;
            _input.Gameplay.RightMouseClick.performed += CancelSelection;
            _input.Gameplay.EndTurn.performed += _turnsHandler.ChangeTurnManually;
            _input.Gameplay.ChangeUnit.performed += ChangeActiveUnit;
            _input.Gameplay.RedrawCard.performed += RedrawCard;

            _input.Gameplay.Enable();
        }

        private void DeAttachInput()
        {
            _input.Gameplay.Disable();

            _input.Gameplay.RotateCameraToLeft.performed -= _camera.RotateToLeft;
            _input.Gameplay.RotateCameraToRight.performed -= _camera.RotateToRight;
            _input.Gameplay.MoveCamera.performed -= _camera.StartMoving;
            _input.Gameplay.MoveCamera.canceled -= _camera.StopMoving;
            _input.Gameplay.MousePosition.performed -= _camera.UpdateMousePosition;
            _input.Gameplay.MouseClick.performed -= Select;
            _input.Gameplay.RightMouseClick.performed -= CancelSelection;
            _input.Gameplay.EndTurn.performed -= _turnsHandler.ChangeTurnManually;
            _input.Gameplay.ChangeUnit.performed -= ChangeActiveUnit;
            _input.Gameplay.RedrawCard.performed -= RedrawCard;
        }

        private void SubscribeToEvents()
        {
            _actionsExecutor.ExecutionStarted += _ui.Hide;
            _actionsExecutor.ExecutionStarted += FollowUnit;
            _actionsExecutor.ExecutionStarted += _turnsHandler.PlayerTurn.DeactivatePlayer;
            _actionsExecutor.ExecutionCompleted += _turnsHandler.PlayerTurn.ActivatePlayer;
            _actionsExecutor.ExecutionCompleted += _turnsHandler.EnemyTurn.OnUnitActed;
            _actionsExecutor.ExecutionCompleted += _camera.StopFollowing;
            _actionsExecutor.ExecutionCompleted += _mouse.ResetState;
            _actionsExecutor.ExecutionCompleted += _ui.Show;

            _turnsHandler.PlayerTurn.Activated += _deck.Hand.UpdateCards;
            _turnsHandler.PlayerTurn.Activated += _ui.Show;
            _turnsHandler.PlayerTurn.Activated += _rules.RestoreDefaults;
            _turnsHandler.PlayerTurn.Ended += _ui.Hide;

            _player.HeroismGained += _rules.IncreaseValue;
            _player.HeroismUsed += _rules.DecreaseValue;
            _player.SkillUsed += _rules.DecreaseValue;
            _player.Moved += _rules.DecreaseValue;
            _player.RequestTargetSelection += _targetSelector.Start;
            _player.RequestHero += _unitsHandler.GetHeroByType;
            _player.CardUsed += _deck.Hand.Remove;
            _player.UnitChanged += _navigationMarker.UpdateMarkerData;
            _player.UnitDataChanged += _ui.UpdateUnitData;
            _player.TargetsSelected += _mouse.ExplicitlyChangeStateToSelected;

            _deck.Hand.RequestRulesData += _rules.GetData;
            _deck.Hand.Redrawed += _rules.DecreaseValue;
            _deck.Hand.CardAdded += _handView.Add;
            _deck.Hand.CardRemoved += _handView.Remove;
            _deck.Hand.CardSelected += _player.OnCardSelected;
            _deck.Hand.CardDeselected += _handView.ResetSelection;

            _handView.RequestRulesData += _rules.GetData;
            _handView.CardSelected += _deck.Hand.SelectCard;

            _mouse.StateChanged += _navigationMarker.UpdateMarkerState;
            _mouse.StateChanged += _player.OnMouseStateChanged;
            _mouse.StateChanged += _handView.UpdateCardsState;
            _mouse.RequestRulesData += _rules.GetData;
            _mouse.RequestPosition += _camera.MouseToWorldPosition;
            _mouse.RequestTarget += _viewAndPointerEventsHandler.PointerTarget;
            _mouse.TargetSelection += _targetSelector.Update;

            _targetSelector.Selected += _player.UseSkill;

            _camera.MousePositionUpdated += _navigationMarker.UpdateMarker;
            
            _navigationMarker.RequestMousePosition += _camera.MouseToWorldPosition;
            _navigationMarker.RequestUnitData += _viewAndPointerEventsHandler.PointerTarget;

            _rules.ValueUpdated += _ui.UpdateRuleValue;

            _unitsHandler.Units.ForEach(unit =>
            {
                unit.AddActionToQueue += _actionsExecutor.Add;
                unit.ExecuteActionsInQueue += _actionsExecutor.Execute;
            });
        }

        private void UnsubscribeToEvents()
        {
            _actionsExecutor.ExecutionStarted -= _ui.Hide;
            _actionsExecutor.ExecutionStarted -= FollowUnit;
            _actionsExecutor.ExecutionStarted -= _turnsHandler.PlayerTurn.DeactivatePlayer;
            _actionsExecutor.ExecutionCompleted -= _turnsHandler.PlayerTurn.ActivatePlayer;
            _actionsExecutor.ExecutionCompleted -= _turnsHandler.EnemyTurn.OnUnitActed;
            _actionsExecutor.ExecutionCompleted -= _camera.StopFollowing;
            _actionsExecutor.ExecutionCompleted -= _mouse.ResetState;
            _actionsExecutor.ExecutionCompleted -= _ui.Show;
            
            _turnsHandler.PlayerTurn.Activated -= _deck.Hand.UpdateCards;
            _turnsHandler.PlayerTurn.Activated -= _ui.Show;
            _turnsHandler.PlayerTurn.Activated -= _rules.RestoreDefaults;
            
            _player.HeroismGained -= _rules.IncreaseValue;
            _player.HeroismUsed -= _rules.DecreaseValue;
            _player.SkillUsed -= _rules.DecreaseValue;
            _player.Moved -= _rules.DecreaseValue;
            _player.RequestTargetSelection -= _targetSelector.Start;
            _player.RequestHero -= _unitsHandler.GetHeroByType;
            _player.CardUsed -= _deck.Hand.Remove;
            _player.UnitChanged -= _navigationMarker.UpdateMarkerData;
            _player.UnitDataChanged -= _ui.UpdateUnitData;
            _player.TargetsSelected -= _mouse.ExplicitlyChangeStateToSelected;

            _deck.Hand.RequestRulesData -= _rules.GetData;
            _deck.Hand.Redrawed -= _rules.DecreaseValue;
            _deck.Hand.CardAdded -= _handView.Add;
            _deck.Hand.CardRemoved -= _handView.Remove;
            _deck.Hand.CardSelected -= _player.OnCardSelected;
            _deck.Hand.CardDeselected -= _handView.ResetSelection;

            _handView.RequestRulesData -= _rules.GetData;
            _handView.CardSelected -= _deck.Hand.SelectCard;
            
            _mouse.StateChanged -= _navigationMarker.UpdateMarkerState;
            _mouse.StateChanged -= _player.OnMouseStateChanged;
            _mouse.StateChanged -= _handView.UpdateCardsState;
            _mouse.RequestRulesData -= _rules.GetData;
            _mouse.RequestPosition -= _camera.MouseToWorldPosition;
            _mouse.RequestTarget -= _viewAndPointerEventsHandler.PointerTarget;
            _mouse.TargetSelection -= _targetSelector.Update;
            
            _targetSelector.Selected -= _player.UseSkill;

            _camera.MousePositionUpdated -= _navigationMarker.UpdateMarker;

            _navigationMarker.RequestMousePosition -= _camera.MouseToWorldPosition;
            _navigationMarker.RequestUnitData -= _viewAndPointerEventsHandler.PointerTarget;
            
            _rules.ValueUpdated -= _ui.UpdateRuleValue;
            
            _unitsHandler.Units.ForEach(unit =>
            {
                unit.AddActionToQueue -= _actionsExecutor.Add;
                unit.ExecuteActionsInQueue -= _actionsExecutor.Execute;
            });
        }

        private void FollowUnit() => _camera.StartFollowing(_actionsExecutor.Sender.Transform);
        
        private void Select(InputAction.CallbackContext obj)
        {
            _mouse.Select();
        }
        
        private void CancelSelection(InputAction.CallbackContext obj)
        {
            _mouse.Deselect();
            _deck.Hand.DeselectCard();
        }
        
        private void ChangeActiveUnit(InputAction.CallbackContext obj)
        {
            Hero hero = _unitsHandler.GetNextHero();
            
            _player.ChangeControllableUnit(hero);
        }
        
        private void RedrawCard(InputAction.CallbackContext obj)
        {
            if (_rules.Redraws > 0 && _handView.IsCardHovered)
                _deck.Hand.RedrawCard(_handView.CoveredCardIndex);
        }
    }
}
