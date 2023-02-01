using UnityEngine;
using GGJ23.Gameplay;
using GGJ23.Input;

namespace ProjectLight.Player
{
    public class PlayerControllerTopDown : MonoBehaviour
    {
        [System.Serializable]
        public struct PlayerMovementParameters
        {
            public float m_Acceleration;
            public float m_Deceleration;
            public float m_MaxSpeed;
        }

        [SerializeField] private ControllerInput m_Input = null;
        [Header("Movement parameters")]
        [SerializeField] private PlayerMovementParameters m_MovementParameters = default;

        private FloatingMovement m_Movement = null;
        private CapsuleCollider2D m_Collider = null;
        private bool m_IsRunning = false;
        private Vector2 m_Velocity = default;
        private float m_SpeedSign = default;

        #region Unity callbacks
        private void Awake()
        {
            m_Movement = GetComponent<FloatingMovement>();
            m_Collider = GetComponentInChildren<CapsuleCollider2D>();
        }

        private void Start()
        {
            m_IsRunning = true;
        }


        private void FixedUpdate()
        {
            if (!m_IsRunning) return;

            MovePlayer();
        }

        #endregion

        #region Player functions
        private void MovePlayer()
        {
            if (m_Input.m_MovementAxes.sqrMagnitude > 0.1f)
            {
                m_Velocity.x += m_Input.m_MovementAxes.x * m_MovementParameters.m_Acceleration *
                                    m_MovementParameters.m_MaxSpeed * Time.deltaTime;

                m_Velocity.y += m_Input.m_MovementAxes.y * m_MovementParameters.m_Acceleration * m_MovementParameters.m_MaxSpeed * Time.deltaTime;
                m_Velocity = Vector2.ClampMagnitude(m_Velocity, m_MovementParameters.m_MaxSpeed);
            }
            else
            {
                if (m_Velocity.sqrMagnitude > 0.1f)
                {
                    if (Mathf.Abs(m_Velocity.x) > 0.1f)
                    {
                        m_SpeedSign = Mathf.Sign(m_Velocity.x);
                        m_Velocity.x += m_MovementParameters.m_Deceleration * Time.deltaTime * -Mathf.Sign(m_Velocity.x);
                        if (Mathf.Sign(m_Velocity.x) != m_SpeedSign)
                        {
                            m_Velocity.x = 0;
                        }
                    }

                    if (Mathf.Abs(m_Velocity.y) > 0.1f)
                    {
                        m_SpeedSign = Mathf.Sign(m_Velocity.y);
                        m_Velocity.y += m_MovementParameters.m_Deceleration * Time.deltaTime * -Mathf.Sign(m_Velocity.y);
                        if (Mathf.Sign(m_Velocity.y) != m_SpeedSign)
                        {
                            m_Velocity.y = 0;
                        }
                    }
                }
                else
                {
                    m_Velocity = Vector2.zero;
                }
            }

            m_Movement.Velocity = m_Velocity;

        }

        public void DisablePlayer()
        {
            m_Velocity = Vector3.zero;
            m_Collider.enabled = false;
            m_IsRunning = false;
        }

        #endregion
    }
}