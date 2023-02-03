using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ23.Input
{
    public class InputManager : MonoBehaviour
    {
        [System.Serializable]
        public class ButtonState
        {
            // TODO: recentlyPressed w/ private counter?
            public bool m_Pressed;
            public bool m_Released;
            public bool m_Held;
    
            public void Reset() => m_Pressed = m_Released = m_Held = false;

            public void UpdateState(InputAction state)
            {
                m_Pressed = state.WasPressedThisFrame();
                m_Released = state.WasReleasedThisFrame();
                m_Held = state.IsPressed();
            }
        }

        public enum ControlScheme
        {
            KeyboardAndMouse,
            Gamepad
        }

        private const string KEYBOARD_AND_MOUSE = "Keyboard&Mouse";
        private const string GAMEPAD = "Gamepad";

        [SerializeField]
        private ControllerInput m_ControllerInput = null;

        private PlayerInput m_PlayerInput = null;
        // Player actions
        private InputAction m_JumpButtonState = null;
        private InputAction m_MoveState = null;

        // UI Actions
        private InputAction m_UINavigateState = null;
        private InputAction m_UISubmitState = null;
        private InputAction m_UICancelState = null;
        private InputAction m_MousePositionState = null;
        private InputAction m_MouseClickState = null;

        //private InputAction m_UIStartGameState = null;
        //private InputAction m_UICreditsState = null;

        private void Awake()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            
            m_JumpButtonState = m_PlayerInput.actions["Jump"];
            m_MoveState = m_PlayerInput.actions["Move"];

            m_UINavigateState = m_PlayerInput.actions["Navigate"];
            m_UISubmitState = m_PlayerInput.actions["Submit"];
            m_UICancelState = m_PlayerInput.actions["Cancel"];
            m_MousePositionState = m_PlayerInput.actions["MousePosition"];
            m_MouseClickState = m_PlayerInput.actions["MouseClick"];
            //m_UIStartGameState = m_PlayerInput.actions["StartGame"];
            //m_UICreditsState = m_PlayerInput.actions["Credits"];
        }

    private void Start()
    {
        m_ControllerInput.ResetState();
    }

    private void Update()
    {
        m_ControllerInput.m_JumpButton.UpdateState(m_JumpButtonState);
        m_ControllerInput.m_MovementAxes = m_MoveState.ReadValue<Vector2>();

        m_ControllerInput.m_UINavigate = m_UINavigateState.ReadValue<Vector2>();
        m_ControllerInput.m_UISubmit.UpdateState(m_UISubmitState);
        m_ControllerInput.m_UICancel.UpdateState(m_UICancelState);
        m_ControllerInput.m_MousePosition = m_MousePositionState.ReadValue<Vector2>();
        m_ControllerInput.m_MouseClick.UpdateState(m_MouseClickState);
        //m_ControllerInput.m_UIStartGame.UpdateState(m_UIStartGameState);
        //m_ControllerInput.m_UICredits.UpdateState(m_UICreditsState);

        m_ControllerInput.m_ControlScheme =
            m_PlayerInput.currentControlScheme switch
            {
                KEYBOARD_AND_MOUSE => ControlScheme.KeyboardAndMouse,
                GAMEPAD => ControlScheme.Gamepad,
                _ => ControlScheme.KeyboardAndMouse
            };
        }
    }
}