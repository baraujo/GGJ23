using GGJ23.Input;
using UnityEngine;

namespace GGJ23.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private ControllerInput m_Input;

        private void Update()
        {
            if (m_Input.m_MouseClick.m_Pressed)
            {
                Debug.Log(Camera.main.ScreenToWorldPoint(m_Input.m_MousePosition) - transform.position);
            }
        }
    }
}
