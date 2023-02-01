using UnityEngine;

namespace GGJ23.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FloatingMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 m_Velocity = default;

        private Rigidbody2D m_Rigidbody = null;
        private Vector3 m_Displacement = default;

        public Vector2 Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            m_Displacement = transform.position;
        }

        void FixedUpdate()
        {
            m_Displacement.x = m_Velocity.x * Time.fixedDeltaTime;
            m_Displacement.y = m_Velocity.y * Time.fixedDeltaTime;

            m_Rigidbody.MovePosition(transform.position + m_Displacement);
        }
    }
}