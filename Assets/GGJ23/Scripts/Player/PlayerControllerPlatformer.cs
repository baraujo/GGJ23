using Prime31;
using GGJ23.Input;

using UnityEngine;

namespace GGJ23.Controllers 
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControllerPlatformer : MonoBehaviour {

        [Header("Movement parameters")]
        public float m_RunSpeed;
        public int m_JumpHeight;
        public float m_Gravity;
        public float m_GroundDamping; // how fast do we change direction? higher is faster
        public float m_AirDamping; // ditto

        public ControllerInput m_Input;

        private CharacterController2D m_Controller;
        private float m_NormalizedHorizontalSpeed;
        [SerializeField] private Vector2 m_Velocity;
        
        // Child components
        //private Animator m_Animator;

        private void Awake() 
        {
            m_Controller = GetComponent<CharacterController2D>();
            //m_Animator = GetComponentInChildren<Animator>();
        }

        void Start() {
            m_Velocity = Vector2.zero;
        }

        void Update() {
            ProcessInput();            
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void LateUpdate() {
            //m_Animator.SetFloat("HorizontalSpeed", Mathf.Abs(m_Velocity.x));
            //m_Animator.SetBool("InAir", Mathf.Abs(m_Velocity.y) > 0.01f);
        }

        private void ProcessInput() {
            if (m_Controller.isGrounded) {
                if (m_Input.m_JumpButton.m_Pressed) 
                {
                    m_Velocity.y = Mathf.Sqrt(2f * m_JumpHeight * -m_Gravity);
                }
            }
            else
            {
                if (!m_Input.m_JumpButton.m_Held && m_Velocity.y > 2)
                {
                    m_Velocity.y = 2;
                }
            }
            if (m_Input.m_MovementAxes.x < 0) {
                m_NormalizedHorizontalSpeed = -1;
                if (transform.localScale.x != -1) FlipHorizontalScale();
            }
            else if (m_Input.m_MovementAxes.x > 0) {
                m_NormalizedHorizontalSpeed = 1;
                if (transform.localScale.x != 1) FlipHorizontalScale();
            }
            else m_NormalizedHorizontalSpeed = 0;
        }

        private void MovePlayer() {
            var dampingFactor = m_Controller.isGrounded ? m_GroundDamping : m_AirDamping;
            m_Velocity.x = Mathf.SmoothDamp(
                m_Velocity.x,
                m_NormalizedHorizontalSpeed * m_RunSpeed,
                ref m_Velocity.x,
                dampingFactor);
            m_Velocity.y += m_Gravity * Time.fixedDeltaTime;

            m_Controller.move(m_Velocity * Time.fixedDeltaTime);
            m_Velocity = m_Controller.velocity;

        }

        private void FlipHorizontalScale() {
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        }
    }
}