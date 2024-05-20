//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/05_input/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Walking"",
            ""id"": ""3f8e84e7-1c16-42b8-b8f1-ca7fad18ffea"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""783ccb1e-6dec-4259-9e7d-41967cbc4c52"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TryToJump"",
                    ""type"": ""Value"",
                    ""id"": ""e9e3fb8e-676d-4521-a300-89101794d0df"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveVR"",
                    ""type"": ""Value"",
                    ""id"": ""ef05af7f-29e9-42cc-861f-960f2bdee59a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""de79a83e-db68-4112-9428-770be4891fa1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ea60ce30-9f79-4f61-99ff-0d29c059a175"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4fa3bc28-c594-4644-8ab1-7da0bc8b2b68"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6b7bcd13-78c1-447c-ada2-7e43da17c007"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f052aa54-d6f9-4d82-9f9f-5e64dfe5abe9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6fe9da6a-4eab-4e53-9784-b54ae6f0843c"",
                    ""path"": ""<OculusTouchController>/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TryToJump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3d7d9e0-5c06-46df-bd44-1041459e5d68"",
                    ""path"": ""<OculusTouchController>{LeftHand}/thumbstick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Running"",
            ""id"": ""d2e29107-28a6-45f9-a62c-e4fdd6e3d236"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""101d8860-c325-41fd-87c2-b196b2d2c209"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TryToJump"",
                    ""type"": ""Button"",
                    ""id"": ""067def12-b273-4b17-891e-87f13de5e386"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TryToSlide"",
                    ""type"": ""Button"",
                    ""id"": ""95fb91eb-8b8b-4983-bc72-466ba22f624e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""8b46d2dc-030c-4cf8-978d-00d3022ed7ae"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b69f7a1a-aebb-4196-84e1-76eb3e8f193a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bad7a3a7-d81f-45db-9f77-42d337fbfd1d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9080e00f-be72-4854-83a8-191739577d59"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""17e71d5c-9821-4b65-adc1-08bc55aae14d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7d211529-76cf-45c7-9191-2d313a1cce53"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TryToJump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86dc81ad-347c-40df-8f4c-eea0c70594e9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TryToSlide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Sliding"",
            ""id"": ""51330e62-bd1e-4704-bb70-3b541143bb66"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""2d416c62-4353-455e-9356-f837e3507dc1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5ac553bd-a50a-4a04-bd4e-fe2759fd5367"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Jumping"",
            ""id"": ""221a86c8-1351-4cdf-b1dc-8116215b8fc6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""dcbb1aa5-c39c-4132-970a-d066aa070bb1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""ed1644aa-9f16-4168-a4aa-9cb808a00dad"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e4d7a6a0-f614-4dbc-97d4-9ee3dc3a2d8d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""df24a8f2-633d-41a8-9f3d-24324dcf21c1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6b75fbb7-b305-430a-95c6-8bf09d4f1844"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4d23c7e6-6e71-4c0f-a039-59e0a142cb44"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Idle"",
            ""id"": ""2df56d23-a17c-4dcb-882d-08fd20eb4cf7"",
            ""actions"": [
                {
                    ""name"": ""MoveVR"",
                    ""type"": ""Value"",
                    ""id"": ""43aa2a7a-a17d-4026-a399-f800ee80443d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4bb48d96-c43d-4cad-a6ff-aaabbac15375"",
                    ""path"": ""<OculusTouchController>{LeftHand}/thumbstick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Respawing"",
            ""id"": ""d38b9dcc-e71b-4e75-8393-b9427069e693"",
            ""actions"": [
                {
                    ""name"": ""TryToRespawn"",
                    ""type"": ""Button"",
                    ""id"": ""f3510651-fd8f-47e9-a1c5-9cc559e2dc33"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e4db1ed8-94c5-4864-8b33-00c8406683c9"",
                    ""path"": ""<OculusTouchController>{LeftHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TryToRespawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Walking
        m_Walking = asset.FindActionMap("Walking", throwIfNotFound: true);
        m_Walking_Move = m_Walking.FindAction("Move", throwIfNotFound: true);
        m_Walking_TryToJump = m_Walking.FindAction("TryToJump", throwIfNotFound: true);
        m_Walking_MoveVR = m_Walking.FindAction("MoveVR", throwIfNotFound: true);
        // Running
        m_Running = asset.FindActionMap("Running", throwIfNotFound: true);
        m_Running_Move = m_Running.FindAction("Move", throwIfNotFound: true);
        m_Running_TryToJump = m_Running.FindAction("TryToJump", throwIfNotFound: true);
        m_Running_TryToSlide = m_Running.FindAction("TryToSlide", throwIfNotFound: true);
        // Sliding
        m_Sliding = asset.FindActionMap("Sliding", throwIfNotFound: true);
        m_Sliding_Move = m_Sliding.FindAction("Move", throwIfNotFound: true);
        // Jumping
        m_Jumping = asset.FindActionMap("Jumping", throwIfNotFound: true);
        m_Jumping_Move = m_Jumping.FindAction("Move", throwIfNotFound: true);
        // Idle
        m_Idle = asset.FindActionMap("Idle", throwIfNotFound: true);
        m_Idle_MoveVR = m_Idle.FindAction("MoveVR", throwIfNotFound: true);
        // Respawing
        m_Respawing = asset.FindActionMap("Respawing", throwIfNotFound: true);
        m_Respawing_TryToRespawn = m_Respawing.FindAction("TryToRespawn", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Walking
    private readonly InputActionMap m_Walking;
    private List<IWalkingActions> m_WalkingActionsCallbackInterfaces = new List<IWalkingActions>();
    private readonly InputAction m_Walking_Move;
    private readonly InputAction m_Walking_TryToJump;
    private readonly InputAction m_Walking_MoveVR;
    public struct WalkingActions
    {
        private @PlayerInputActions m_Wrapper;
        public WalkingActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Walking_Move;
        public InputAction @TryToJump => m_Wrapper.m_Walking_TryToJump;
        public InputAction @MoveVR => m_Wrapper.m_Walking_MoveVR;
        public InputActionMap Get() { return m_Wrapper.m_Walking; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WalkingActions set) { return set.Get(); }
        public void AddCallbacks(IWalkingActions instance)
        {
            if (instance == null || m_Wrapper.m_WalkingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_WalkingActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @TryToJump.started += instance.OnTryToJump;
            @TryToJump.performed += instance.OnTryToJump;
            @TryToJump.canceled += instance.OnTryToJump;
            @MoveVR.started += instance.OnMoveVR;
            @MoveVR.performed += instance.OnMoveVR;
            @MoveVR.canceled += instance.OnMoveVR;
        }

        private void UnregisterCallbacks(IWalkingActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @TryToJump.started -= instance.OnTryToJump;
            @TryToJump.performed -= instance.OnTryToJump;
            @TryToJump.canceled -= instance.OnTryToJump;
            @MoveVR.started -= instance.OnMoveVR;
            @MoveVR.performed -= instance.OnMoveVR;
            @MoveVR.canceled -= instance.OnMoveVR;
        }

        public void RemoveCallbacks(IWalkingActions instance)
        {
            if (m_Wrapper.m_WalkingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IWalkingActions instance)
        {
            foreach (var item in m_Wrapper.m_WalkingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_WalkingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public WalkingActions @Walking => new WalkingActions(this);

    // Running
    private readonly InputActionMap m_Running;
    private List<IRunningActions> m_RunningActionsCallbackInterfaces = new List<IRunningActions>();
    private readonly InputAction m_Running_Move;
    private readonly InputAction m_Running_TryToJump;
    private readonly InputAction m_Running_TryToSlide;
    public struct RunningActions
    {
        private @PlayerInputActions m_Wrapper;
        public RunningActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Running_Move;
        public InputAction @TryToJump => m_Wrapper.m_Running_TryToJump;
        public InputAction @TryToSlide => m_Wrapper.m_Running_TryToSlide;
        public InputActionMap Get() { return m_Wrapper.m_Running; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RunningActions set) { return set.Get(); }
        public void AddCallbacks(IRunningActions instance)
        {
            if (instance == null || m_Wrapper.m_RunningActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_RunningActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @TryToJump.started += instance.OnTryToJump;
            @TryToJump.performed += instance.OnTryToJump;
            @TryToJump.canceled += instance.OnTryToJump;
            @TryToSlide.started += instance.OnTryToSlide;
            @TryToSlide.performed += instance.OnTryToSlide;
            @TryToSlide.canceled += instance.OnTryToSlide;
        }

        private void UnregisterCallbacks(IRunningActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @TryToJump.started -= instance.OnTryToJump;
            @TryToJump.performed -= instance.OnTryToJump;
            @TryToJump.canceled -= instance.OnTryToJump;
            @TryToSlide.started -= instance.OnTryToSlide;
            @TryToSlide.performed -= instance.OnTryToSlide;
            @TryToSlide.canceled -= instance.OnTryToSlide;
        }

        public void RemoveCallbacks(IRunningActions instance)
        {
            if (m_Wrapper.m_RunningActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IRunningActions instance)
        {
            foreach (var item in m_Wrapper.m_RunningActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_RunningActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public RunningActions @Running => new RunningActions(this);

    // Sliding
    private readonly InputActionMap m_Sliding;
    private List<ISlidingActions> m_SlidingActionsCallbackInterfaces = new List<ISlidingActions>();
    private readonly InputAction m_Sliding_Move;
    public struct SlidingActions
    {
        private @PlayerInputActions m_Wrapper;
        public SlidingActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Sliding_Move;
        public InputActionMap Get() { return m_Wrapper.m_Sliding; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SlidingActions set) { return set.Get(); }
        public void AddCallbacks(ISlidingActions instance)
        {
            if (instance == null || m_Wrapper.m_SlidingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SlidingActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(ISlidingActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(ISlidingActions instance)
        {
            if (m_Wrapper.m_SlidingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISlidingActions instance)
        {
            foreach (var item in m_Wrapper.m_SlidingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SlidingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SlidingActions @Sliding => new SlidingActions(this);

    // Jumping
    private readonly InputActionMap m_Jumping;
    private List<IJumpingActions> m_JumpingActionsCallbackInterfaces = new List<IJumpingActions>();
    private readonly InputAction m_Jumping_Move;
    public struct JumpingActions
    {
        private @PlayerInputActions m_Wrapper;
        public JumpingActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Jumping_Move;
        public InputActionMap Get() { return m_Wrapper.m_Jumping; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JumpingActions set) { return set.Get(); }
        public void AddCallbacks(IJumpingActions instance)
        {
            if (instance == null || m_Wrapper.m_JumpingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_JumpingActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IJumpingActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IJumpingActions instance)
        {
            if (m_Wrapper.m_JumpingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IJumpingActions instance)
        {
            foreach (var item in m_Wrapper.m_JumpingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_JumpingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public JumpingActions @Jumping => new JumpingActions(this);

    // Idle
    private readonly InputActionMap m_Idle;
    private List<IIdleActions> m_IdleActionsCallbackInterfaces = new List<IIdleActions>();
    private readonly InputAction m_Idle_MoveVR;
    public struct IdleActions
    {
        private @PlayerInputActions m_Wrapper;
        public IdleActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveVR => m_Wrapper.m_Idle_MoveVR;
        public InputActionMap Get() { return m_Wrapper.m_Idle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(IdleActions set) { return set.Get(); }
        public void AddCallbacks(IIdleActions instance)
        {
            if (instance == null || m_Wrapper.m_IdleActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_IdleActionsCallbackInterfaces.Add(instance);
            @MoveVR.started += instance.OnMoveVR;
            @MoveVR.performed += instance.OnMoveVR;
            @MoveVR.canceled += instance.OnMoveVR;
        }

        private void UnregisterCallbacks(IIdleActions instance)
        {
            @MoveVR.started -= instance.OnMoveVR;
            @MoveVR.performed -= instance.OnMoveVR;
            @MoveVR.canceled -= instance.OnMoveVR;
        }

        public void RemoveCallbacks(IIdleActions instance)
        {
            if (m_Wrapper.m_IdleActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IIdleActions instance)
        {
            foreach (var item in m_Wrapper.m_IdleActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_IdleActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public IdleActions @Idle => new IdleActions(this);

    // Respawing
    private readonly InputActionMap m_Respawing;
    private List<IRespawingActions> m_RespawingActionsCallbackInterfaces = new List<IRespawingActions>();
    private readonly InputAction m_Respawing_TryToRespawn;
    public struct RespawingActions
    {
        private @PlayerInputActions m_Wrapper;
        public RespawingActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @TryToRespawn => m_Wrapper.m_Respawing_TryToRespawn;
        public InputActionMap Get() { return m_Wrapper.m_Respawing; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RespawingActions set) { return set.Get(); }
        public void AddCallbacks(IRespawingActions instance)
        {
            if (instance == null || m_Wrapper.m_RespawingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_RespawingActionsCallbackInterfaces.Add(instance);
            @TryToRespawn.started += instance.OnTryToRespawn;
            @TryToRespawn.performed += instance.OnTryToRespawn;
            @TryToRespawn.canceled += instance.OnTryToRespawn;
        }

        private void UnregisterCallbacks(IRespawingActions instance)
        {
            @TryToRespawn.started -= instance.OnTryToRespawn;
            @TryToRespawn.performed -= instance.OnTryToRespawn;
            @TryToRespawn.canceled -= instance.OnTryToRespawn;
        }

        public void RemoveCallbacks(IRespawingActions instance)
        {
            if (m_Wrapper.m_RespawingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IRespawingActions instance)
        {
            foreach (var item in m_Wrapper.m_RespawingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_RespawingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public RespawingActions @Respawing => new RespawingActions(this);
    public interface IWalkingActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnTryToJump(InputAction.CallbackContext context);
        void OnMoveVR(InputAction.CallbackContext context);
    }
    public interface IRunningActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnTryToJump(InputAction.CallbackContext context);
        void OnTryToSlide(InputAction.CallbackContext context);
    }
    public interface ISlidingActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IJumpingActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IIdleActions
    {
        void OnMoveVR(InputAction.CallbackContext context);
    }
    public interface IRespawingActions
    {
        void OnTryToRespawn(InputAction.CallbackContext context);
    }
}
