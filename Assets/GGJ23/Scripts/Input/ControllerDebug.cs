using UnityEngine;

namespace GGJ23.Input
{
    public class ControllerDebug : MonoBehaviour
    {
        [SerializeField] private ControllerInput m_Input = null;

        private void Update()
        {
            if (m_Input.m_JumpButton.m_Pressed)
            {
                Debug.Log("Action button pressed");
            }
            //if (m_Input.m_JumpButton.m_Held)
            //{
            //    Debug.Log("Action button held");
            //}
            if (m_Input.m_JumpButton.m_Released)
            {
                Debug.Log("Action button released");
            }
        }
    }
}