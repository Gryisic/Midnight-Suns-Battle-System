// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerInput/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace PlayerInput
{
    public class @Input : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Input()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""e682f522-f912-4c37-aecd-dd1f386a6ae9"",
            ""actions"": [
                {
                    ""name"": ""RotateCameraToRight"",
                    ""type"": ""Button"",
                    ""id"": ""c274c89a-5528-4009-9645-e5b4aade5a0c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCameraToLeft"",
                    ""type"": ""Button"",
                    ""id"": ""abac4f12-f34b-49e1-99c7-f2cfbd1edef5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""e94362b6-db2f-4117-92fb-95f1117f9c3f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""86d8ede0-c141-4cb5-91c1-261f1b40b083"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""4e94360d-c3a7-42f5-8c60-f0fb77d1a71a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EndTurn"",
                    ""type"": ""Button"",
                    ""id"": ""e26549a3-87c2-4854-9532-923ab8d04ff7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightMouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""031e18d0-df02-4c51-9e35-95337025c15e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeUnit"",
                    ""type"": ""Button"",
                    ""id"": ""08f75532-0f32-45b9-90c0-00428cdb0a5f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RedrawCard"",
                    ""type"": ""Button"",
                    ""id"": ""7032f530-de01-436b-beb0-a8b3dc488173"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ba5abbd8-06c3-422d-8df4-db1d320cf198"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraToRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17f9fc3f-9cdf-4c88-acda-5b229902db1e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCameraToLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3521b1e3-c92e-4137-a26b-b38627cd4512"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0fd991fa-9a6b-424e-a8c5-8cedeb077011"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""726d0769-b077-4a7a-a21c-d5c6c4e3eecd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""98cc48de-4981-471a-a27b-611560db0842"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""067b92f3-e187-429a-a087-f1772fd7d4ce"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d46231a2-c7ab-4eb4-a5f9-7abbd4615ecb"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e18e720-cca6-4651-b83d-ddb25249c668"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f40af8d-18cc-4e77-80d3-74593113dc97"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EndTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a47bc5c4-4d46-4eda-9f37-3080cc92c998"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightMouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cae9afa1-386d-4c69-a31b-1a6aca501130"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3808eb3b-a800-43e9-b587-c3dfc50200c1"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RedrawCard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_RotateCameraToRight = m_Gameplay.FindAction("RotateCameraToRight", throwIfNotFound: true);
            m_Gameplay_RotateCameraToLeft = m_Gameplay.FindAction("RotateCameraToLeft", throwIfNotFound: true);
            m_Gameplay_MoveCamera = m_Gameplay.FindAction("MoveCamera", throwIfNotFound: true);
            m_Gameplay_MousePosition = m_Gameplay.FindAction("MousePosition", throwIfNotFound: true);
            m_Gameplay_MouseClick = m_Gameplay.FindAction("MouseClick", throwIfNotFound: true);
            m_Gameplay_EndTurn = m_Gameplay.FindAction("EndTurn", throwIfNotFound: true);
            m_Gameplay_RightMouseClick = m_Gameplay.FindAction("RightMouseClick", throwIfNotFound: true);
            m_Gameplay_ChangeUnit = m_Gameplay.FindAction("ChangeUnit", throwIfNotFound: true);
            m_Gameplay_RedrawCard = m_Gameplay.FindAction("RedrawCard", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_RotateCameraToRight;
        private readonly InputAction m_Gameplay_RotateCameraToLeft;
        private readonly InputAction m_Gameplay_MoveCamera;
        private readonly InputAction m_Gameplay_MousePosition;
        private readonly InputAction m_Gameplay_MouseClick;
        private readonly InputAction m_Gameplay_EndTurn;
        private readonly InputAction m_Gameplay_RightMouseClick;
        private readonly InputAction m_Gameplay_ChangeUnit;
        private readonly InputAction m_Gameplay_RedrawCard;
        public struct GameplayActions
        {
            private @Input m_Wrapper;
            public GameplayActions(@Input wrapper) { m_Wrapper = wrapper; }
            public InputAction @RotateCameraToRight => m_Wrapper.m_Gameplay_RotateCameraToRight;
            public InputAction @RotateCameraToLeft => m_Wrapper.m_Gameplay_RotateCameraToLeft;
            public InputAction @MoveCamera => m_Wrapper.m_Gameplay_MoveCamera;
            public InputAction @MousePosition => m_Wrapper.m_Gameplay_MousePosition;
            public InputAction @MouseClick => m_Wrapper.m_Gameplay_MouseClick;
            public InputAction @EndTurn => m_Wrapper.m_Gameplay_EndTurn;
            public InputAction @RightMouseClick => m_Wrapper.m_Gameplay_RightMouseClick;
            public InputAction @ChangeUnit => m_Wrapper.m_Gameplay_ChangeUnit;
            public InputAction @RedrawCard => m_Wrapper.m_Gameplay_RedrawCard;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @RotateCameraToRight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCameraToRight;
                    @RotateCameraToRight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCameraToRight;
                    @RotateCameraToRight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCameraToRight;
                    @RotateCameraToLeft.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCameraToLeft;
                    @RotateCameraToLeft.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCameraToLeft;
                    @RotateCameraToLeft.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotateCameraToLeft;
                    @MoveCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveCamera;
                    @MoveCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveCamera;
                    @MoveCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveCamera;
                    @MousePosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                    @MousePosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                    @MousePosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                    @MouseClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClick;
                    @MouseClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClick;
                    @MouseClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClick;
                    @EndTurn.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndTurn;
                    @EndTurn.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndTurn;
                    @EndTurn.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndTurn;
                    @RightMouseClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightMouseClick;
                    @RightMouseClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightMouseClick;
                    @RightMouseClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightMouseClick;
                    @ChangeUnit.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeUnit;
                    @ChangeUnit.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeUnit;
                    @ChangeUnit.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeUnit;
                    @RedrawCard.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRedrawCard;
                    @RedrawCard.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRedrawCard;
                    @RedrawCard.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRedrawCard;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @RotateCameraToRight.started += instance.OnRotateCameraToRight;
                    @RotateCameraToRight.performed += instance.OnRotateCameraToRight;
                    @RotateCameraToRight.canceled += instance.OnRotateCameraToRight;
                    @RotateCameraToLeft.started += instance.OnRotateCameraToLeft;
                    @RotateCameraToLeft.performed += instance.OnRotateCameraToLeft;
                    @RotateCameraToLeft.canceled += instance.OnRotateCameraToLeft;
                    @MoveCamera.started += instance.OnMoveCamera;
                    @MoveCamera.performed += instance.OnMoveCamera;
                    @MoveCamera.canceled += instance.OnMoveCamera;
                    @MousePosition.started += instance.OnMousePosition;
                    @MousePosition.performed += instance.OnMousePosition;
                    @MousePosition.canceled += instance.OnMousePosition;
                    @MouseClick.started += instance.OnMouseClick;
                    @MouseClick.performed += instance.OnMouseClick;
                    @MouseClick.canceled += instance.OnMouseClick;
                    @EndTurn.started += instance.OnEndTurn;
                    @EndTurn.performed += instance.OnEndTurn;
                    @EndTurn.canceled += instance.OnEndTurn;
                    @RightMouseClick.started += instance.OnRightMouseClick;
                    @RightMouseClick.performed += instance.OnRightMouseClick;
                    @RightMouseClick.canceled += instance.OnRightMouseClick;
                    @ChangeUnit.started += instance.OnChangeUnit;
                    @ChangeUnit.performed += instance.OnChangeUnit;
                    @ChangeUnit.canceled += instance.OnChangeUnit;
                    @RedrawCard.started += instance.OnRedrawCard;
                    @RedrawCard.performed += instance.OnRedrawCard;
                    @RedrawCard.canceled += instance.OnRedrawCard;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        public interface IGameplayActions
        {
            void OnRotateCameraToRight(InputAction.CallbackContext context);
            void OnRotateCameraToLeft(InputAction.CallbackContext context);
            void OnMoveCamera(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
            void OnMouseClick(InputAction.CallbackContext context);
            void OnEndTurn(InputAction.CallbackContext context);
            void OnRightMouseClick(InputAction.CallbackContext context);
            void OnChangeUnit(InputAction.CallbackContext context);
            void OnRedrawCard(InputAction.CallbackContext context);
        }
    }
}
