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
                    ""type"": ""Value"",
                    ""id"": ""783ccb1e-6dec-4259-9e7d-41967cbc4c52"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e9e3fb8e-676d-4521-a300-89101794d0df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Slide"",
                    ""type"": ""Button"",
                    ""id"": ""8e8a90cf-6044-473f-99af-87cf67554409"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Button"",
                    ""id"": ""6fc266d7-1343-4038-b149-8821f7e4e9de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
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
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c35ec46-ed27-4b50-8fb8-e9045d025862"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be4186ea-549b-4cec-be41-d65fdac09759"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9ee3fc4-e83f-4d39-bc07-0e4277e3468a"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Map2"",
            ""id"": ""366c35c2-edaf-44f3-a5a2-270073689f16"",
            ""actions"": [
                {
                    ""name"": ""Move2"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cdb7c2e0-31cd-4f4c-bbf0-a86ac641f22c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3ab0ea6d-7df0-44af-b26b-19a3a5314b26"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Map1"",
            ""id"": ""7c35db4e-761a-4356-8e96-9abf6efe83df"",
            ""actions"": [
                {
                    ""name"": ""Move1"",
                    ""type"": ""Value"",
                    ""id"": ""8d06605f-f2d1-4f13-8ee9-66244a93fcbd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e03470d0-0991-4e86-8a3f-1fac5748957c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move1"",
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
        m_Walking_Jump = m_Walking.FindAction("Jump", throwIfNotFound: true);
        m_Walking_Slide = m_Walking.FindAction("Slide", throwIfNotFound: true);
        m_Walking_Turn = m_Walking.FindAction("Turn", throwIfNotFound: true);
        // Map2
        m_Map2 = asset.FindActionMap("Map2", throwIfNotFound: true);
        m_Map2_Move2 = m_Map2.FindAction("Move2", throwIfNotFound: true);
        // Map1
        m_Map1 = asset.FindActionMap("Map1", throwIfNotFound: true);
        m_Map1_Move1 = m_Map1.FindAction("Move1", throwIfNotFound: true);
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
    private readonly InputAction m_Walking_Jump;
    private readonly InputAction m_Walking_Slide;
    private readonly InputAction m_Walking_Turn;
    public struct WalkingActions
    {
        private @PlayerInputActions m_Wrapper;
        public WalkingActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Walking_Move;
        public InputAction @Jump => m_Wrapper.m_Walking_Jump;
        public InputAction @Slide => m_Wrapper.m_Walking_Slide;
        public InputAction @Turn => m_Wrapper.m_Walking_Turn;
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
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Slide.started += instance.OnSlide;
            @Slide.performed += instance.OnSlide;
            @Slide.canceled += instance.OnSlide;
            @Turn.started += instance.OnTurn;
            @Turn.performed += instance.OnTurn;
            @Turn.canceled += instance.OnTurn;
        }

        private void UnregisterCallbacks(IWalkingActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Slide.started -= instance.OnSlide;
            @Slide.performed -= instance.OnSlide;
            @Slide.canceled -= instance.OnSlide;
            @Turn.started -= instance.OnTurn;
            @Turn.performed -= instance.OnTurn;
            @Turn.canceled -= instance.OnTurn;
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

    // Map2
    private readonly InputActionMap m_Map2;
    private List<IMap2Actions> m_Map2ActionsCallbackInterfaces = new List<IMap2Actions>();
    private readonly InputAction m_Map2_Move2;
    public struct Map2Actions
    {
        private @PlayerInputActions m_Wrapper;
        public Map2Actions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move2 => m_Wrapper.m_Map2_Move2;
        public InputActionMap Get() { return m_Wrapper.m_Map2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Map2Actions set) { return set.Get(); }
        public void AddCallbacks(IMap2Actions instance)
        {
            if (instance == null || m_Wrapper.m_Map2ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Map2ActionsCallbackInterfaces.Add(instance);
            @Move2.started += instance.OnMove2;
            @Move2.performed += instance.OnMove2;
            @Move2.canceled += instance.OnMove2;
        }

        private void UnregisterCallbacks(IMap2Actions instance)
        {
            @Move2.started -= instance.OnMove2;
            @Move2.performed -= instance.OnMove2;
            @Move2.canceled -= instance.OnMove2;
        }

        public void RemoveCallbacks(IMap2Actions instance)
        {
            if (m_Wrapper.m_Map2ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMap2Actions instance)
        {
            foreach (var item in m_Wrapper.m_Map2ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Map2ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Map2Actions @Map2 => new Map2Actions(this);

    // Map1
    private readonly InputActionMap m_Map1;
    private List<IMap1Actions> m_Map1ActionsCallbackInterfaces = new List<IMap1Actions>();
    private readonly InputAction m_Map1_Move1;
    public struct Map1Actions
    {
        private @PlayerInputActions m_Wrapper;
        public Map1Actions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move1 => m_Wrapper.m_Map1_Move1;
        public InputActionMap Get() { return m_Wrapper.m_Map1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Map1Actions set) { return set.Get(); }
        public void AddCallbacks(IMap1Actions instance)
        {
            if (instance == null || m_Wrapper.m_Map1ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Map1ActionsCallbackInterfaces.Add(instance);
            @Move1.started += instance.OnMove1;
            @Move1.performed += instance.OnMove1;
            @Move1.canceled += instance.OnMove1;
        }

        private void UnregisterCallbacks(IMap1Actions instance)
        {
            @Move1.started -= instance.OnMove1;
            @Move1.performed -= instance.OnMove1;
            @Move1.canceled -= instance.OnMove1;
        }

        public void RemoveCallbacks(IMap1Actions instance)
        {
            if (m_Wrapper.m_Map1ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMap1Actions instance)
        {
            foreach (var item in m_Wrapper.m_Map1ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Map1ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Map1Actions @Map1 => new Map1Actions(this);
    public interface IWalkingActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSlide(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
    }
    public interface IMap2Actions
    {
        void OnMove2(InputAction.CallbackContext context);
    }
    public interface IMap1Actions
    {
        void OnMove1(InputAction.CallbackContext context);
    }
}
