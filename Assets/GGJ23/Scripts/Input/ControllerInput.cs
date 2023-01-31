using UnityEngine;
using static GGJ23.Input.InputManager;

namespace GGJ23.Input
{
    [CreateAssetMenu(menuName = "GGJ23/ControllerInput")]
    public class ControllerInput : ScriptableObject
    {
        // Player
        public Vector2 m_MovementAxes;
        public ButtonState m_JumpButton;
        public ControlScheme m_ControlScheme;

        // UI
        public Vector2 m_UINavigate;
        public ButtonState m_UISubmit;
        public ButtonState m_UICancel;
        //public ButtonState m_UIStartGame;
        //public ButtonState m_UICredits;

        private void OnEnable() => ResetState();
        private void OnDisable() => ResetState();

        public void ResetState()
        {
            m_MovementAxes = Vector2.zero;
            m_JumpButton.Reset();
            
            m_UINavigate = Vector2.zero;
            m_UISubmit.Reset();
            m_UICancel.Reset();
            //m_UIStartGame.Reset();
            //m_UICredits.Reset();
        }
    } 
}